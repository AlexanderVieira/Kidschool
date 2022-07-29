using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Extensions;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Data
{
    public class ChildDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public DbSet<Child> Children { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        public ChildDbContext(DbContextOptions<ChildDbContext> options,IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<Educator>();            

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

                        property.SetColumnType("varchar(50)");
                        property.SetValueConverter(converter);
                    }
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChildDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Educator>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now.ToLocalTime();
                        //entry.Entity.CreatedBy = "Admin";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now.ToLocalTime();
                        //entry.Entity.LastModifiedBy = "Admin";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents_v2(this);
            return success;
        }

        public Task<bool> Commit(bool commited)
        {
            throw new NotImplementedException();
        }
    }
}
