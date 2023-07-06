using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Student : IEquatable<Student>
    {


        public string Name { get; }
        
        public string Surname { get; }
        
        public string Patronymic { get; }
        
        public string Group { get; }

        public string Course { get; }

        public Student(string Name, string Surname, string Patronymic, string Group, string Course)
        {
            string[] values = new string[5];
            values[0] = Name;
            values[1] = Surname;
            values[2] = Patronymic;
            values[3] = Group;
            values[4] = Course;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {
                    throw new ArgumentNullException(nameof(values));
                }
            }
            this.Name = values[0];
            this.Surname = values[1];
            this.Patronymic = values[2];
            this.Group = values[3];
            this.Course = values[4];
        }

        public override string ToString()
        {
            return Name + " " + Surname + " " + Patronymic + " " + Group + " " + Course;
        }

        public override bool Equals(object obj)
        {
            if(obj.ToString() == (Name + " " + Surname + " " + Patronymic + " " + Group + " " + Course))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
