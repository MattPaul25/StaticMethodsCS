using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //overloading operators
            var student1 = new student(1) { name = "Matt" };
            var student2 = new student(2) { name = "Nick" };
            student student3 = student1 + student2;
            student student4 = student3 + 3;

            student1.Display();
            student2.Display();
            student3.Display();
            student4.Display();

            student1.Grade = 98;
            student2.Grade = 87;

            Console.WriteLine((student1 > student2).ToString()); //returns true with overloaded comparative operator
            Console.WriteLine((student1 < student2).ToString());
            int y = student4; //implicit conversion
            Console.WriteLine("converted student implicitly: " + y.ToString());
            Console.ReadLine();
        }
       
    }

    class student
    {
        public string name { get; set; }
        public int id { get; set; }
        public int Grade { get; set; }

        public student(int id)
        {
            this.id = id;
        }
        public static student operator +(student s1, student s2)
        {
            //allows the creation of new student via the plus operator
            student newStudent = new student(s1.id + s2.id);
            newStudent.name = "nothing";
            return newStudent;
        }

        public static student operator +(student s1, int x)
        {
            //allows the student object to be added to a number
            student newStudent = new student(s1.id + x);
            newStudent.name = "nothing";
            return newStudent;
        }
        public static bool operator >(student s1, student s2)
        {
            //allows the object to use the greater than operator
            if (s1.Grade > s2.Grade)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(student s1, student s2)
        {
            //allows the object to use the less than operator
            if (s1.Grade < s2.Grade)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static implicit operator int(student s1)
        {
            //converts the object into a value implicitly
            return s1.id;
        }

        public void Display()
        {
            Console.WriteLine("name:" + this.name);
            Console.WriteLine("id: " + this.id.ToString());
        }
    }
}
