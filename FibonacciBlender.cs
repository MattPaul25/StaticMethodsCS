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
        static int[] origSet = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 
                                   14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 
                                   24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 
                                   34, 35, 36, 37, 38, 39, 40 };
        static void Main(string[] args)
        {   
            //unOrder
            string printString = "";
            int[] swappedSet;
            swappedSet = Swap(origSet, 2);
            swappedSet = FibbyBlender(swappedSet, 9);
            for (int j = 0; j < origSet.Length; j++)
            {
               printString = printString + " " + swappedSet[j].ToString();
            }
            Console.WriteLine(printString);
            Console.ReadLine();

            //ReOrder
            
            swappedSet = FibbyReOrder(swappedSet, 9);
            swappedSet = Swap(swappedSet, 2);
            printString = "";
            for (int j = 0; j < origSet.Length; j++)
            {
                printString = printString + " " + swappedSet[j].ToString();
            }
            Console.WriteLine(printString);
            Console.ReadLine();
        }
        private static int[] FibbyBlender(int[] origSet, int finNum)
        {
            int holder;
            int[] newSet = origSet;
            int[] blendedSet = new int[origSet.Length];
            for (int i = 0; i < finNum; i++)
            {
                int swapNum = fibby(i);
                blendedSet[i] = swapNum;

                if (blendedSet[i] != 0)
                {
                    holder = newSet[i];
                    newSet[i] = newSet[blendedSet[i]];
                    newSet[blendedSet[i]] = holder;
                }
                else if (newSet[i] == 0)
                {
                    newSet[i] = origSet[i];
                }
            }
            return newSet;
        }
        private static int fibby(int i)
        {
            int cnt = 0;
            int lead = 1;
            int behind = 0;
            int holder;
            while (cnt <= i)
            {
                holder = behind;
                behind = lead;
                if ((holder + behind) > origSet.Length) { return 0; }
                lead = holder + behind;
                cnt++;
            }
            return lead;
        }
        private static int[] FibbyReOrder(int[] origSet, int finNum)
        {
            int holder;
            int[] newSet = origSet;
            int[] blendedSet = new int[origSet.Length];
            for (int i = finNum; i >= 0; i--)
            {
                int swapNum = fibby(i);
                blendedSet[i] = swapNum;
                if (blendedSet[i] != 0)
                {
                    holder = newSet[i];
                    newSet[i] = newSet[blendedSet[i]];
                    newSet[blendedSet[i]] = holder;
                }
                else if (newSet[i] == 0)
                {
                    newSet[i] = origSet[i];
                }
            }
            return newSet;
        }
        private static int fibbyDown(int i)
        {
            int cnt = 0;
            int lead = 1;
            int behind = 0;
            int holder;
            while (cnt <= i)
            {
                holder = behind;
                behind = lead;
                if ((holder + behind) > origSet.Length) { return 0; }
                lead = holder + behind;
                cnt++;
            }
            return behind;
        }        
        private static int[] Swap(int[]origSet, int sNum)
        {
            int[] NewSet = origSet;
            for (int i = 0; i < NewSet.Length; i = i + sNum + 1 )
            {
                int holder = NewSet[i];
                int j = i + 1;
                try
                {
                    NewSet[i] = NewSet[j];
                    NewSet[j] = holder;
                }
                catch { }
            }
            return NewSet;        
        }
    }

}
