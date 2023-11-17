using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student>? Students { get; set; }
        public DbSet<Group>? Groups { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group { Id=1,Name="22-ISP-31",Speciality="ISP",Faculty="IT"},
                new Group { Id=2,Name = "22-ISP-32", Speciality = "ISP", Faculty = "IT" }
                );
            modelBuilder.Entity<Student>().HasData(
                new Student {Id=1, Name="Гадик", FirstName="Петров",
                LastName="Петрович",BirthDay=new DateTime(2003,12,8),
                GroupId=1},
                new Student
                {
                    Id=2,
                    Name = "Мразик",
                    FirstName = "Ифанов",
                    LastName = "Иванович",
                    BirthDay = new DateTime(2003, 09, 15),
                    GroupId=2
                }
                );
    //        modelBuilder.Entity<User>().HasData(
    //            new User { id=1, EMail= "kosha@mail.ru", Password = "123456" },
    //new User {id=1, EMail = "zinovievmaksim@mail.ru", Password = "11111" }
    //            );
        }
    }
}
