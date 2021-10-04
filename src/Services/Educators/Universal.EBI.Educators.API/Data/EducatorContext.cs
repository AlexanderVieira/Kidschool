﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Extensions;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Data
{
    public class EducatorContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public DbSet<Educator>  Educators { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public EducatorContext(DbContextOptions<EducatorContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EducatorContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }
    }
}