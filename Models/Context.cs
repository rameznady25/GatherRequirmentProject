using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gather_Requirement_Project.Models
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }

        public DbSet<CustomerProgram> CustomerPrograms { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<UserStories> UserStories { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<InputType> InputTypes { get; set; }
        public DbSet<ScreenType> ScreenTypes { get; set; }
        public DbSet<ScreenComponent> ScreenComponent { get; set; }
        public DbSet<Portal> Portal { get; set; }
        public DbSet<MainPortal> MainPortal { get; set; }

        //public DbSet<ScreencomValidations> ScreenComValidation { get; set; }
        //public DbSet<FieldValidation> FieldValidations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScreenType>().HasData(
                  new ScreenType { ID = 1, Name = "Create" },
                  new ScreenType { ID = 2, Name = "Delete" },
                  new ScreenType { ID = 3, Name = "Update" },
                  new ScreenType { ID = 4, Name = "Edit" },
                  new ScreenType { ID = 5, Name = "Search" },
                  new ScreenType { ID = 6, Name = "List" },
                  new ScreenType { ID = 7, Name = "View" },
                  new ScreenType { ID = 8, Name = "Setting" },
                  new ScreenType { ID = 9, Name = "Report" }
            );

            modelBuilder.Entity<InputType>().HasData(
                new InputType { ID = 1, Name = "number" },
                new InputType { ID = 2, Name = "text" },
                new InputType { ID = 3, Name = "bool" },
                new InputType { ID = 4, Name = "email" },
                new InputType { ID = 5, Name = "date" }
             );

            modelBuilder.Entity<FieldType>().HasData(
                new FieldType { ID = 1, Name = "dropDown" },
                new FieldType { ID = 2, Name = "number" },
                new FieldType { ID = 3, Name = "text" },
                new FieldType { ID = 4, Name = "email" },
                new FieldType { ID = 5, Name = "date" },
                new FieldType { ID = 6, Name = "checkBox" },
                new FieldType { ID = 7, Name = "radiButton" },
                new FieldType { ID = 8, Name = "textarea" },
                new FieldType { ID = 9, Name = "file" },
                new FieldType { ID = 10, Name = "button" }
            );


            /*
            modelBuilder.Entity<ScreenComponent>()
                .HasOne(e => e.ScreenComponentDepend)
                .WithMany()
                .HasForeignKey(m => m.ScreenComponentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FieldValidation>().HasData(
                    new FieldValidation { ID = 1, Name = "Required" },
                    new FieldValidation { ID = 2, Name = "Min" },
                    new FieldValidation { ID = 3, Name = "Max" }
            );
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
            */


        }

        //public DbSet<ScreencomValidations> ScreenComValidation { get; set; }
        //public DbSet<FieldValidation> FieldValidations { get; set; }

        public DbSet<Gather_Requirement_Project.Models.Section> Section { get; set; }

        //public DbSet<ScreencomValidations> ScreenComValidation { get; set; }
        //public DbSet<FieldValidation> FieldValidations { get; set; }


        //public DbSet<ScreencomValidations> ScreenComValidation { get; set; }
        //public DbSet<FieldValidation> FieldValidations { get; set; }

        //public DbSet<Gather_Requirement_Project.Models.CentralizedManagement> CentralizedManagement { get; set; }
    }
}