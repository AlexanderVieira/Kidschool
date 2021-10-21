using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Extensions;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Educators.API.Data
{
    public class EducatorContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAspNetUser _user;

        public DbSet<Educator>  Educators { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public EducatorContext(IMediatorHandler mediatorHandler, IAspNetUser user)
        {
            _mediatorHandler = mediatorHandler;
            _user = user;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.BaseType == typeof(Enum))
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                        property.SetValueConverter(converter);
                    }
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EducatorContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Person>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _user?.Name;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _user?.Name;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }

        public Task<bool> Commit(bool commited)
        {
            throw new System.NotImplementedException();
        }
    }
}
