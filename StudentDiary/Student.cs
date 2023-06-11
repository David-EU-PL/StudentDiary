using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDiary
{
    public class Student : Person
    {
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Chemistry { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
    }
    public class Teacher : Person
    {
        public string Position { get; set; }

    }
    public class Person
    {
     
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }

    }
}
        