using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Childs.API.Data.Mapping
{
    public class ResponsableMapping : IEntityTypeConfiguration<Responsible>
    {
        public void Configure(EntityTypeBuilder<Responsible> builder)
        {
            builder.HasKey(r => r.Id);            

            builder.Property(r => r.FirstName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(r => r.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(r => r.FullName)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(r => r.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                .IsRequired()                
                .HasConversion<string>(x => x.ToString(), x => new Cpf(x).Number)
                .HasMaxLength(Cpf.CPF_MAX_LENGTH)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CPF_MAX_LENGTH})");                

            });

            builder.OwnsOne(r => r.Email, tf =>
            {
                tf.Property(e => e.Address)
                .IsRequired()
                .HasConversion<string>(x => x.ToString(), x => new Email(x).Address)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");                

            });

            builder.HasMany(r => r.Children)
                .WithMany(c => c.Responsibles);

            builder.HasOne(r => r.Address)
               .WithOne(a => a.Responsible);

            builder.HasMany(r => r.Phones)
                .WithOne(p => p.Responsible);

            builder.ToTable("Responsibles");
        }
    }
}
