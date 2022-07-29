using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Childs.API.Data.Mapping
{
    public class ChildMapping : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.HasKey(c => c.Id);            

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasColumnType("varchar(200)");

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
                .HasConversion<string>(c => c.ToString(), c => new Email(c).Address)
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.ADDRESS_MAX_LENGTH})");                

            });

            builder.HasMany(c => c.Responsibles)
                .WithMany(r => r.Children);

            builder.HasOne(c => c.Address)
               .WithOne(a => a.Child);

            builder.HasMany(c => c.Phones)
                .WithOne(p => p.Child);

            builder.HasOne(c => c.Classroom)
                .WithMany(cl => cl.Children);

            builder.ToTable("Children");
        }
    }
}
