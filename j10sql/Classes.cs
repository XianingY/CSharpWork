using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace j10sql
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
    }

    public class Class
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; } // Foreign key to School
        public List<Student> Students { get; set; } = new List<Student>();
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public int ClassId { get; set; } // Foreign key to Class
    }

}
