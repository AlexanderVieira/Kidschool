using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Reports.API.Extensions;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Data
{
    public class ReportContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Educator>  Educators { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }        
        
        //public DbSet<ChildResponsible> ChildResponsible { get; set; }
        public ReportContext(DbContextOptions<ReportContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportContext).Assembly);

           // modelBuilder.Entity<Educator>().HasData(
            
           //     new Educator
           //     {
           //         Id = Guid.Parse("9b9e4944-b91f-41e9-a8d8-252c9ec7f815"),
           //         FirstName = "Josilene",
           //         LastName = "Silva de Sales",
           //         FullName = "Josilene Silva de Sales",
           //         FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), "Responsible", true),
           //         GenderType = (GenderType)Enum.Parse(typeof(GenderType), "F", true),
           //         Excluded = false,
           //         ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a")
           //     }
           // );

           // modelBuilder.Entity<Child>().HasData(

           //     new Child
           //     {
           //         Id = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f"),
           //         FirstName = "Jonathan",
           //         LastName = "de Sales da Silva",
           //         FullName = "Jonathan de Sales da Silva",
           //         BirthDate = new DateTime(2012,8,29).Date,
           //         GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
           //         AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), "Juniors", true),
           //         StartTimeMeeting = DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
           //         EndTimeMeeting = DateTime.Parse(DateTime.UtcNow.AddHours(1.5).ToString("HH:mm:ss")),
           //         ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
           //         Excluded = false
                    
           //         //ChildResponsibles = new List<ChildResponsible> { new ChildResponsible { ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"), ChildId = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f") } }
           //     }

           // );

           // modelBuilder.Entity<ChildResponsible>().HasData(

           //     new ChildResponsible
           //     {
           //         ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
           //         ChildId = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f")
           //     }

           // );

           // modelBuilder.Entity<Responsible>().HasData(

           //     new Responsible
           //     {
           //         Id = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"),
           //         //Cpf = new Cpf("04138000755"),
           //         FirstName = "Alexander",
           //         LastName = "Vieira da Silva",
           //         FullName = "Alexander Vieira da Silva",
           //         KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), "Dad", true),
           //         GenderType = (GenderType)Enum.Parse(typeof(GenderType), "M", true),
           //         Excluded = false,
           //         //Phones = new List<Phone> { new Phone { Number = "21965200293", ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f") } },
           //         //ChildResponsibles = new List<ChildResponsible> { new ChildResponsible { ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f"), ChildId = Guid.Parse("e0c7a17a-d202-4d34-a31b-1b0f348a3e1f") } }
           //     }

           // );

           // modelBuilder.Entity<Classroom>().HasData(

           //    new Classroom
           //    {
           //        Id = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a"),
           //        Region = "São João I",
           //        Church = "São Mateus II",
           //        CreatedAt = DateTime.UtcNow.Date,
           //        MeetingTime = DateTime.Parse(DateTime.UtcNow.ToString("HH:mm:ss")),
           //        //Educator = new Educator { FirstName = "Josilene", ClassroomId = Guid.Parse("5811275e-de3e-4f33-81f2-3c7a92d1c27a") },
           //        ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), "Mixed", true),
           //        Lunch = "Iogurt e Biscoito"
           //    }

           //);

           // modelBuilder.Entity<Phone>().HasData(

           //     new Phone
           //     {
           //         Id = Guid.Parse("5b03891a-dbe2-45ac-81b9-381b381ca01d"),
           //         Number = "21965200293",
           //         PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), "CellPhone", true),
           //         ResponsibleId = Guid.Parse("d2df1d8a-3456-40e0-b7d2-5939f403095f")
           //     }
           // );

           // Task<bool> task = Commit();
            //base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(this);
            return success;
        }

        public Task<bool> Commit(bool commited)
        {
            throw new System.NotImplementedException();
        }
    }
}
