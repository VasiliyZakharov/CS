using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IEquatable<Student> student1 = new Student("Vasya", "Zakharov", "Anatolevich", "M8O-208B-21", "c#");
            Student student2 = new Student("Vasya", "Zakharov", "Anatolevich", "M8O-208B-21", "c#");
            Console.WriteLine(student2.YearOfEducation());
            Console.WriteLine(student1.ToString());
            Console.WriteLine(student1.Equals(student2));
            Console.ReadKey();
        }
    }
}
