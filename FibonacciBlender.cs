using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibbonacciBlend
{
    class Program
    {
        //blends the array by positions via indecis created by fibbonacci sequence
        public static string MyString = "This is a typical String. Its got letters and Numbers (like 4), along with punctation! ";              
        static void Main(string[] args)
        {
            //mucks it up
            string aString = outIn(MyString);
            Console.WriteLine(aString);
            Console.ReadLine();
            aString = inOut(aString);
            Console.WriteLine(aString);
            Console.ReadLine();

            Console.WriteLine("Please Write a String");
            string ConsoleString = Console.ReadLine();
            Console.WriteLine(ConsoleString);
            Console.WriteLine();
            Console.WriteLine("Lets merge the string");
            ConsoleString = mergeText(ConsoleString);
            Console.WriteLine("After Merging: " + ConsoleString);
            Console.ReadLine();
            ConsoleString = FibbyBlender(ConsoleString);
            Console.WriteLine("After FibBlend: " + ConsoleString);
            Console.ReadLine();
            ConsoleString = Swap(ConsoleString, 1);
            Console.WriteLine("After Swap: " + ConsoleString);
            Console.ReadLine();
            Console.WriteLine("The procedures to clean the file up start after enter...");
            Console.ReadLine();
            ////puts it back together
            ConsoleString = Swap(ConsoleString, 1);
            Console.WriteLine("After Re-swap: " + ConsoleString);
            Console.ReadLine();
            ConsoleString = FibbyReOrder(ConsoleString); //must be higher than fibby blender order of magnitude
            Console.WriteLine("After Fib-ReOrder: " + ConsoleString);
            Console.ReadLine();
            ConsoleString = unMergeText(ConsoleString);
            Console.WriteLine("After UnMerging the String: " + ConsoleString);
            Console.ReadLine();
        }
        private static string mergeText(string someText)
        {
            int strLength = someText.Length - 1 ;
            for (int i = 0; i <= strLength; i = i + 2)
            {
                string myChar = someText.Substring(strLength, 1);
                string left = Left(someText, i);
                string right = Right(someText, i);
                someText = left + myChar + right;
                someText = someText.Substring(0, strLength + 1);
            }
            return someText;
        }
        private static string unMergeText(string someText)
        {
            string[] myStringArr = new string[someText.Length];
            int strLength = someText.Length - 1;
            int cntD = strLength;
            int cntU = 0;
            for (int i = 0; i <= strLength; i = i +2)
            {
                string myChar = someText.Substring(i,1);
                int x = i + 1;
                try
                {
                    string myChar2 = someText.Substring(x, 1);
                    myStringArr[cntU] = myChar2;
                }
                catch { }
                myStringArr[cntD] = myChar;
                cntD--;
                cntU++;
            }
            someText = convertArray(myStringArr);
            return someText;
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
        static string Left(string yourString, int PlaceMent)
        {
            return yourString.Substring(0, PlaceMent);
        }
        static string Right(string yourString, int PlaceMent)
        {
            return yourString.Substring(PlaceMent, yourString.Length - PlaceMent);
        }
        private static string FibbyBlender(string someText)
        {
            string holder;
            string[] origSet = SimpleSplit(someText);
            string[] newSet = origSet;
            int[] blendedSet = new int[newSet.Length];
            for (int i = 0; i < someText.Length; i++)
            {
                int swapChar = fibby(i, someText);
                blendedSet[i] = swapChar;
                if (blendedSet[i] != 0)
                {
                    holder = newSet[i];
                    newSet[i] = newSet[blendedSet[i]];
                    newSet[blendedSet[i]] = holder;
                }
                else if (newSet[i] == "")
                {
                    newSet[i] = origSet[i];
                }
            }
            return convertArray(newSet);
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
        private static int fibby(int i, string someText)
        {
            int cnt = 0;
            int lead = 1;
            int behind = 0;
            int holder;
            while (cnt <= i)
            {
                holder = behind;
                behind = lead;
                if ((holder + behind) > someText.Length) { return 0; }
                lead = holder + behind;
                cnt++;
            }
            return lead;
        }
        private static string FibbyReOrder(string someText)
        {
            string holder;
            string[] origSet = SimpleSplit(someText);
            string[] newSet = origSet;
            int[] blendedSet = new int[newSet.Length];
            for (int i = (someText.Length - 1); i >= 0; i--)
            {
                int swapNum = fibby(i, someText);
                blendedSet[i] = swapNum;
                if (blendedSet[i] != 0)
                {
                    holder = newSet[i];
                    newSet[i] = newSet[blendedSet[i]];
                    newSet[blendedSet[i]] = holder;
                }
                else if (newSet[i] == "")
                {
                    newSet[i] = origSet[i];
                }
            }
            return convertArray(newSet);
        }
        private static int fibbyDown(int i, string someText)
        {
            int cnt = 0;
            int lead = 1;
            int behind = 0;
            int holder;
            while (cnt <= i)
            {
                holder = behind;
                behind = lead;
                if ((holder + behind) > someText.Length) { return 0; }
                lead = holder + behind;
                cnt++;
            }
            return behind;
        }        
        private static string Swap(string someText, int sNum)
        {
            string[] NewSet = SimpleSplit(someText);
            for (int i = 0; i < NewSet.Length; i = i + sNum + 1 )
            {
                string holder = NewSet[i];
                int j = i + 1;
                try
                {
                    NewSet[i] = NewSet[j];
                    NewSet[j] = holder;
                }
                catch { }
            }
            return convertArray(NewSet);        
        }
        private static string outIn(string someText)
        {   
            int x ;
            int i = 0;
            int len = someText.Length;
            if (len % 2 != 1)  
            {
                someText = someText + " ";                 
            }
            len = someText.Length;
            x = (len + 1) / 2;
            while (i < x)
            {
               string lMove = someText.Substring(0, 1);
               string rMove = someText.Substring(len - 1, 1);
               someText = someText.Insert(len - x, rMove);
               someText = someText.Substring(0, len);               
               someText = someText.Insert(x, lMove);
               someText = someText.Substring(1, len);
               i++;
            }
            if (someText.Substring(len - 1, 1) == " ")
            { someText = someText.Substring(0, len - 2); }
            return someText;
        }
        private static string inOut(string someText)
        {
            int x;
            int i = 0;
            int len = someText.Length;
            if (len % 2 != 1)
            {
                someText = someText + " ";
            }
            x = (len + 1) / 2;
            while (i < x)
            {               
                string lMove = someText.Substring(x- 1, 1);
                someText = someText.Remove(x - 1, 1);
                someText = someText.Insert(0, lMove);               
                string rMove = someText.Substring(x - 1, 1); //
                someText = someText.Remove(x - 1, 1);
                someText = someText.Insert(len- 1, rMove); 
                i++;
            }
            if (someText.Substring(len - 1, 1) == " " && (someText.Length % 2) != 1 )
            { someText = someText.Substring(0, len - 1); }
            return someText;

        }
    }
}
