using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndLambdaDemo
{

    class Student
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Student(string name)
        {
            Name = name;

            DateOfBirth = DateTime.Now;
        }
    }



    class Program
    {

        static void Main(string[] args)
        {
            // Extension methods defined in MyLinqExtensions.cs.
            // Go check it out.

            string[] strings = { "aaa", "b", "cc", "dddd" };

            bool anyStartsWithL = strings.AnyStartsWithL();
            bool anyStartsWithM = strings.AnyStartsWithM();

            bool betterAny = strings.Any(s => s.StartsWith("l"));
            bool betterAny2 = strings.Any(s => s.StartsWith("m"));
            bool betterAny3 = strings.Any(s => s.StartsWith("a"));
            bool betterAny4 = strings.Any(s => s.Length > 2);


            var students = new List<Student>(){
                new Student("Luka"),
                new Student("Mario"),
                new Student("Igor"),
            };

            bool anyLukas = students.Any(s => s.Name == "Luka");
            bool anyBirthdays = students.Any(s => s.DateOfBirth.Month == DateTime.Now.Month && s.DateOfBirth.Day == DateTime.Now.Day);




        }
    }


}
