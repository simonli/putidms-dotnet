using Putidms.Domain.Models;
using System.Data.Entity;

namespace Putidms.Domain.DAL
{
    public class EFDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Klass> Klasses { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<KlassRecord> KlassRecords { get; set; }
        public DbSet<TrainingRecord> TrainingRecords { get; set; }
        public DbSet<EvaluationRecord> EvaluationRecords { get; set; }


        public EFDbContext() : base("putidms") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Department>().HasRequired(x => x.Division)
            //    .WithMany().HasForeignKey(x => x.DivisionId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Klass>().HasRequired(x => x.Department)
            //    .WithMany().HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Counselor>().HasRequired(x => x.Klass)
            //    .WithMany().HasForeignKey(x => x.KlassId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Counselor>().HasRequired(x => x.Duty)
            //    .WithMany().HasForeignKey(x => x.DutyId).WillCascadeOnDelete(false);

            modelBuilder.Entity<KlassRecord>().HasRequired(x => x.Klass)
                .WithMany().HasForeignKey(x => x.KlassId).WillCascadeOnDelete(false);

            modelBuilder.Entity<KlassRecord>().HasRequired(x => x.Duty)
                .WithMany().HasForeignKey(x => x.DutyId).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasRequired(x => x.Division)
                .WithMany().HasForeignKey(x => x.DivisionId).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasRequired(x => x.Department)
                .WithMany().HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(false);
        }

    }
}