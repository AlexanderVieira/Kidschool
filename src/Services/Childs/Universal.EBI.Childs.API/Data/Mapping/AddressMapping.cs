using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Data.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.PublicPlace)
                //.IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.Number)
                //.IsRequired()
                .HasColumnType("varchar(50)");            

            builder.Property(a => a.ZipCode)
                //.IsRequired()
                .HasColumnType("varchar(20)");

            builder.HasOne(a => a.Child)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>(a => a.ChildId);

            builder.HasOne(a => a.Responsible)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>(a => a.ResponsibleId);

            builder.ToTable("Addresses");

        }
    }
}
