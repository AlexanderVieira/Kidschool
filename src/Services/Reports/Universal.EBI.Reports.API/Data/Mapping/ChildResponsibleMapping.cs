using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Data.Mapping
{
    //public class ChildResponsibleMapping : IEntityTypeConfiguration<ChildResponsible>
    //{
    //    public void Configure(EntityTypeBuilder<ChildResponsible> builder)
    //    {

    //        builder.HasKey(sc => new { sc.ChildrenId, sc.ResponsiblesId });

    //        builder.HasOne<Child>(sc => sc.Child)
    //            .WithMany(s => s.Responsibles)
    //            .HasForeignKey(sc => sc.ChildId);


    //        builder.HasOne<Responsible>(sc => sc.Responsible)
    //            .WithMany(s => s.ChildResponsibles)
    //            .HasForeignKey(sc => sc.ResponsibleId);

    //        builder.ToTable("ChildResponsible");
    //    }
    //}
}
