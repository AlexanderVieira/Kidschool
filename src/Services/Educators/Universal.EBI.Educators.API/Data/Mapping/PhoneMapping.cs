using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Data.Mapping
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnType("varchar(13)");
        }
    }
}
