using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo
{
    public class TestDBContext : DbContext
    {
        public DbSet<Student> TestTable2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DACHAU\SQLEXPRESS;Database=testdatabase;Trusted_Connection=True");
        }
    }

    public class Student
    {
        [Key]
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Scolarship { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    class TestClass
    {
        static void Main(string[] args)
        {
            // create();
            // read();
            // update();
            // delete();
        }

        private static void delete()
        {
            using (var db = new TestDBContext())
            {
                var result = db.TestTable2.SingleOrDefault(b => b.Name == "ManualNew");
                if (result != null)
                {
                    db.Remove(result);
                    db.SaveChanges();

                }
            }
        }

        private static void update()
        {
            using (var db = new TestDBContext())
            {
                var result = db.TestTable2.SingleOrDefault(b => b.Name == "New");
                if (result != null)
                {
                    result.Name = "Some new value";
                    db.SaveChanges();

                }
            }
        }

        private static void read()
        {
            using (var db = new TestDBContext())
            {
                var students = db.TestTable2.ToList();

                students.ForEach(student => { Console.WriteLine(student.Name); });
                
            }
        }

        static void create() {

            using (var db = new TestDBContext())
            {
                var student = new Student { Name = "ManualNew", Lastname = "ManualNew", Scolarship = 123, RegistrationDate = DateTime.Now };
                db.TestTable2.Add(student);
                db.SaveChanges();
            }
            
        }
    }

}