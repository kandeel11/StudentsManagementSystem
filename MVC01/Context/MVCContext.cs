using Microsoft.EntityFrameworkCore;

namespace MVC01.Context
{
    public class MVCContext : DbContext
    {
        //public MVCContext(): base() { }
        public MVCContext(DbContextOptions<MVCContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
