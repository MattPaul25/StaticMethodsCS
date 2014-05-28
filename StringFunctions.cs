using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            string eml = "Matt.Farguson@gmail.com";
            string emlDomain = RightOf(eml, "@"); //returns gmail.com
            string emlFirstName = LeftOf(eml, "."); //returns Matt
            string emlLastName = eml.Substring(Search(eml, "."), Search(eml, "@") - Search(eml, ".")-1); //returns last name       
            Console.WriteLine("Matt.Farguson@gmail.com's domain is " + emlDomain);
            Console.WriteLine("Matt.Farguson@gmail.com's first name is " + emlFirstName);
            Console.WriteLine("The email last name is " + emlLastName);
            Console.ReadLine();
        }
        static string LeftOf(string yourString, string yourMarker)
        {
            //method or function that pulls everything left of a unique Marker
            int anum = 0;
            int len = yourString.Length;
            int len2 = yourMarker.Length;
            string newString = "";
            do
            {
                string temp = yourString.Substring(anum, len2);
                if (temp == yourMarker)
                {
                    return newString;
                }
                newString = newString + temp;
                anum += 1;
            } while (anum < len);
            return "";
        }
        static string RightOf(string yourString, string yourMarker)
        {
            //method or function that pulls everything right of a unique Marker
            int len = yourString.Length;
            int len2 = yourMarker.Length;
            int cnt = 0;
            for (int i = (len - len2); i > 0; i--)
            {
                cnt = cnt + 1;
                string temp = yourString.Substring(i, len2);
                if (temp == yourMarker)
                {
                    string newString = yourString.Substring(i + len2, cnt - 1);
                    return newString;
                }            
            }
            return "";
        }
        static int Search(string yourString, string yourMarker, int yourInst = 1, bool caseSensitive = true)
        {
            //returns the placement of a string in another string
            int num = 1;
            int ginst = 1;
            int mlen = yourMarker.Length;
            int slen = yourString.Length;
            string tString = "";
            try
            {
                if (caseSensitive == false)
                {
                    yourString = yourString.ToLower();
                    yourMarker = yourMarker.ToLower();
                }
                while (num < slen)
                {
                    tString = yourString.Substring(num, mlen);

                    if (tString == yourMarker && ginst == yourInst)
                    {
                        return num + 1;
                    }
                    else if (tString == yourMarker && yourInst != ginst)
                    {
                        ginst += 1;
                        num += 1;
                    }
                    else
                    {
                        num += 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }

}
