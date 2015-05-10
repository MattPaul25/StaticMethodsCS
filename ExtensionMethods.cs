using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    //obviously its silly to write extension methods to a class that i have access to - but here is an example
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person() { Name = "Matt", Age = 26 };
            var p2 = new Person() { Name = "Sally", Age = 35 };
            p.SayHello(p2);
            Console.ReadLine();

        }        
    }

    public static class Extentions
    {
        //extension methods must be static
        //they require the this keyword
        public static void SayHello(this Person person, Person person2)
        {
            Console.WriteLine("{0} says hello to {1}", person.Name, person2.Name);
        }
    }


    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
