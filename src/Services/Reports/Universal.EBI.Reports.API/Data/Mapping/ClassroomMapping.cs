using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Data.Mapping
{
    public class ClassroomMapping : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.Region)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(cl => cl.Church)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(cl => cl.Lunch)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.HasOne(e => e.Educator)
                .WithOne(c => c.Classroom)
                .HasPrincipalKey<Classroom>(c => c.Id)
                .HasForeignKey<Educator>(e => e.ClassroomId);

            //builder.HasMany(cl => cl.Children)
            //    .WithOne(c => c.Classroom)
            //    .HasForeignKey(c => c.ClassroomId)
            //    .HasPrincipalKey(cl => cl.Id)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Classrooms");
        }
    }
}
