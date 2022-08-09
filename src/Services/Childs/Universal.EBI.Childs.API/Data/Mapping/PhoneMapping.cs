using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Data.Mapping
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
                //.IsRequired()
                .HasColumnType("varchar(13)");

            builder.HasOne(p => p.Child)
                .WithMany(c => c.Phones);

            builder.HasOne(p => p.Responsible)
                .WithMany(r => r.Phones);                
        }
    }
}
