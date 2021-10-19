using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Data.Mapping
{
    public class ResponsibleMapping : IEntityTypeConfiguration<Responsible>
    {
        public void Configure(EntityTypeBuilder<Responsible> builder)
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

            builder.Property(c => c.Cpf)
                //.IsRequired()
                .HasMaxLength(Cpf.CPF_MAX_LENGTH)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");

            //builder.OwnsOne(c => c.Cpf, tf =>
            //{
            //    tf.Property(c => c.Number)
            //    //.IsRequired()                
            //    .HasMaxLength(Cpf.CPF_MAX_LENGTH)
            //    .HasColumnName("Cpf")
            //    .HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");
            //});

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                //.IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");
            });

            builder.HasOne(r => r.Address)
                .WithOne(a => a.Responsible)
                .HasForeignKey<Address>(a => a.ResponsibleId);

            builder.HasMany(c => c.Phones)
                .WithOne(c => c.Responsible)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(c => c.ResponsibleId);

            builder.HasMany(c => c.Children)
                .WithMany(p => p.Responsibles);

            builder.ToTable("Responsibles");
        }
    }
}
