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
      
        public static string MyString = "Here is a count to the number twenty: 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20";


        static void Main(string[] args)
        {
            //mucks it up
            string myText = mergeText(MyString);
            Console.WriteLine(myText);
            Console.ReadLine();
            myText = FibbyBlender(myText, 12);
            myText = Swap(myText, 3);
            Console.WriteLine(myText);
            Console.ReadLine();

            //puts it back together
            myText = Swap(myText, 3);
            myText = FibbyReOrder(myText, 12);
            myText = unMergeText(myText);
            Console.WriteLine(myText);
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
        private static string FibbyBlender(string someText, int finNum)
        {
            string holder;
            string[] origSet = SimpleSplit(someText);
            string[] newSet = origSet;
            int[] blendedSet = new int[newSet.Length];
            for (int i = 0; i < finNum; i++)
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
        private static string FibbyReOrder(string someText, int finNum)
        {
            string holder;
            string[] origSet = SimpleSplit(someText);
            string[] newSet = origSet;
            int[] blendedSet = new int[newSet.Length];
            for (int i = finNum; i >= 0; i--)
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
    }
}
