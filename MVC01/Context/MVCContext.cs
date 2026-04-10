using Microsoft.EntityFrameworkCore;

namespace MVC01.Context
{
    public class MVCContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;database=MVCDB;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Instructor_Crs>().HasKey(ic => new { ic.Ins_Id, ic.Crs_Id });
            modelBuilder.Entity<Models.Student_Crs>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Models.Student> Students { get; set; }
        public DbSet<Models.Instructor> Instructors { get; set; }
        public DbSet<Models.Department> Departments { get; set; }
        public DbSet<Models.Course> Courses { get; set; }
         public DbSet<Models.Student_Crs> Student_Crs { get; set; }
        public DbSet<Models.Instructor_Crs> Instructor_Crs { get; set; }
    }
}
