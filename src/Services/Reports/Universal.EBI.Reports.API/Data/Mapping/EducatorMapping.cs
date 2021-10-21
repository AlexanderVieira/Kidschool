﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Reports.API.Data.Mapping
{
    public class EducatorMapping : IEntityTypeConfiguration<Educator>
    {
        public void Configure(EntityTypeBuilder<Educator> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar(250)");           

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                //.IsRequired()
                .HasConversion<string>(c => c.ToString(), c => new Cpf(c).Number)
                .HasMaxLength(Cpf.CPF_MAX_LENGTH)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                //.IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");
            });
            
            builder.HasOne(c => c.Address)
                .WithOne(c => c.Educator)
                .HasForeignKey<Address>(a => a.EducatorId);

            builder.HasMany(c => c.Phones)
                .WithOne(c => c.Educator)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(p => p.EducatorId);

            builder.ToTable("Educators");
        }
    }
}
