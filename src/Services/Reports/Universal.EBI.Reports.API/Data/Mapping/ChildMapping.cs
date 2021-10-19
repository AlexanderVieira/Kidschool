using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Data.Mapping
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

            builder.HasOne(c => c.Classroom)
                .WithMany(cl => cl.Children)
                .HasPrincipalKey(cl => cl.Id)
                .HasForeignKey(c => c.ClassroomId);
                //.OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Address)
                .WithOne(a => a.Child)
                .HasPrincipalKey<Child>(c => c.Id)
                .HasForeignKey<Address>(a => a.ChildId);

            builder.HasMany(c => c.Phones)
                .WithOne(p => p.Child)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(p => p.ChildId);

            builder.HasMany(c => c.Responsibles)
                .WithMany(p => p.Children);
                //.HasPrincipalKey<Child>(c => c.Id)
                //.HasForeignKey<Responsible>(p => p.ChildId);


            builder.ToTable("Children");
        }
    }
}
