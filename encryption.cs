using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Program
    {
       public static string[] CryptArr = { "Z", "Y", "X", "W", "V", "U", "T", "S", "R", "Q", "P", "O", "N", "M", "L", "K", "J", "I", "H", "G", "F", "E", "D", "C", "B", "A", "$" };
       public static string[] AlphArr = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " "};
        static void Main(string[] args)
        {
            string myMessage = "try this message";
            string cryptMessage = encryptMessage(myMessage);
            Console.WriteLine(cryptMessage);
            string originalMessage = unEncryptMessage(cryptMessage);
            Console.WriteLine(originalMessage);
            Console.ReadLine();
        }

       private static string encryptMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            string newMessage = "";
            for (int i = 0; i < message.Length; i++)
            {
                string MyChar = message.Substring(i, 1);
                int myIndex = findIndex(MyChar, AlphArr);
                newMessage = newMessage + CryptArr[myIndex];
            }
            return newMessage;
        }
       private static string unEncryptMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            string newMessage = "";
            for (int i = 0; i < message.Length; i++)
            {
                string MyChar = message.Substring(i, 1);
                int myIndex = findIndex(MyChar, CryptArr);
                newMessage = newMessage + AlphArr[myIndex];
            }
            return newMessage;
        }

        private static int findIndex(string p, string[] anArr)
        {
           int x = anArr.Length;
           int i;
            for (i = 0; i < x; i++)
            {
                if (anArr[i] == p)
                {
                    break;
                }
            }
            return i;
        }
    }
}
