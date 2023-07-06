using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public sealed class Student :
        IEquatable<Student>
    {
        public string Name { get; }

        public string Surname { get; }

        public string Patronymic { get; }

        public string Group { get; }

        public string Course { get; }

        public Student(string name, string surname, string patronymic, string group, string course)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
            Group = group ?? throw new ArgumentNullException(nameof(group));
            Course = course ?? throw new ArgumentNullException(nameof(course));
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {Patronymic} {Group} {Course}";
        }

        public bool Equals(
            Student? student)
        {
            if (student == null)
            {
                return false;
            }

            return Name.Equals(student.Name, StringComparison.Ordinal)
                   && Surname.Equals(student.Surname, StringComparison.Ordinal)
                   && Patronymic.Equals(student.Patronymic, StringComparison.Ordinal)
                   && Group.Equals(student.Group, StringComparison.Ordinal)
                   && Course.Equals(student.Course, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Student sz)
            {
                return Equals(sz);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Patronymic, Group, Course);
        }

        public int YearOfEducation()
        {
            int year = 2000;
            year += ((int)Group[9] - 48) * 10;
            year += ((int)Group[10] - 48);
            year = 2023 - year;
            return year;
        }
    }
}
