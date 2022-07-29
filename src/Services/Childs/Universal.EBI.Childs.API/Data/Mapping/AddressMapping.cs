using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Core.DomainObjects.Models;

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

            builder.ToTable("Addresses");

        }
    }
}
