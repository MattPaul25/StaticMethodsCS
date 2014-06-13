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
        static int[] origSet = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30};
        static void Main(string[] args)
        {
            int[] swappedSet = Blender(origSet, 8);
        }

        private static int[] Blender(int[] origSet, int finNum)
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

    }
}
