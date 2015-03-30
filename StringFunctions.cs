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
          
          static string insertText(string yourInsertString, string yourString, int placeMent)
        {
            if (yourString == "")
            {
                return yourInsertString;
            }
            else if (placeMent == 1)
            {
                return yourInsertString + yourString;            
            }
            else if (placeMent >= yourString.Length)
            {
                return yourString + yourInsertString;
            }
            else
            {
                string a = Left(yourString, placeMent);
                string b = Right(yourString, placeMent);
                return a + yourInsertString + b;
            }
        }

        static string Left(string yourString, int PlaceMent)
        {
            return yourString.Substring(1, PlaceMent);
        }
        static string Right(string yourString, int PlaceMent)
        {
            return yourString.Substring(PlaceMent, yourString.Length - PlaceMent);
        }
          public static int Search(string yourString, string yourMarker, int yourInst = 1, bool caseSensitive = true)
       {
           //returns the placement of a string in another string
           int num = 0;
           int currentInst = 1;
           int markerLength = yourMarker.Length;
           int stringLength = yourString.Length;
          
           if (!caseSensitive)
           {
               //if optional argument, case sensitive is false convert string and marker to lowercase
               yourString = yourString.ToLower();
               yourMarker = yourMarker.ToLower();
           }
           try
           {    
               bool found = false;           
               while (num < stringLength)
               {
                  string testString = yourString.Substring(num, markerLength);
                  num += 1;

                   if (testString == yourMarker)
                   {
                       if (currentInst == yourInst)
                       {
                           found = true;
                           break;
                       }
                       currentInst++;
                   }
                   num++;
               }              
           }
           catch
           {
               num = 0;
           }
           num = found ? num : 0;
           return num;
       }

        static int CountString(string yourString, string yourMarker)
        {   
            int cnt = 0;
            int mLen = yourMarker.Length;
            for (int i = 1; i <= yourString.Length; i++)
            {
                if (yourString.Substring(i, mLen) == yourMarker)
                { 
                    cnt++; 
                }
            }
            return cnt;
        }
        
        public static int CountString2(string yourString, string yourMarker)
        {
            //counts the number of strings that exist in another string
            int myCnt = 0;
            string newstring = yourString.Replace(yourMarker, "");
            myCnt = (yourString.Length - newstring.Length) / yourMarker.Length;
            return myCnt;
        }       
        
         public static int FindIndex(string p, string[] anArr)
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
         private static string[] SimpleSplit(string aString) 
        {
            string[] newArr = new string[aString.Length];

            for (int i = 0; i < aString.Length; i++)            
            {
                newArr[i] = aString.Substring(i, 1);            
            }
            return newArr;
        }
        
        private static string convertArray(string[] stringArr)
        {
            string newString = "";
            int i;
            for (i = 0; i < stringArr.Length; i++) 
            {
                newString = newString + stringArr[i];
            }
            return newString; 
        }
    }
}
