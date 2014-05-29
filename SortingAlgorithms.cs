using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime tim = DateTime.Now;
            int[] nums = { 10, 8, 6, 5, 19, 2090, 1, 0, 18, 100, 30, 14, 14, 13, 2, 4, 9000};
            //nums = boolSort(nums);
            nums = bubbleSort(nums);
            //nums = insertionSort(nums);
            System.TimeSpan nTime = DateTime.Now - tim;
            Console.WriteLine(nTime.ToString());
            int y = nums.Length;
            for (int tes = 0; tes < y; tes++)
            {
                Console.WriteLine(nums[tes].ToString());
            }

            Console.ReadLine();
        }
        static int[] bubbleSort(int[] nums)
        {
            int y = nums.Length;
            for (int z = 0; z < (y - z); z++)
            {
                int x = 0;
                for (int i = x + 1; i < y; i++)
                {
                    if (nums[i] < nums[x])
                    {
                        int l = nums[i];
                        nums[i] = nums[x];
                        nums[x] = l;
                    }
                    x++;
                }
            }
            return nums;
        }
        static int[] boolSort(int[] nums)
        {
            int nLen = nums.Length;
            int[] nArray = new int[nLen];

            for (int i = 0; i < nLen; i++)
            {
                int x = 0;
                int count = 0;
                while (x < nLen)
                {
                    if (nums[i] > nums[x] || (nums[i] == nums[x] && x > i))
                    { count++; }
                    x++;
                }
                nArray[count] = nums[i];
            }
            return nArray;
        }

        static int[] insertionSort(int[] nums)
        {
            int nLen = nums.Length;
            for (int i = 1; i < nLen; i++)
            {
                int y = i;
                int x = i - 1;
                while (x >= 0)
                {
                    if (nums[y] < nums[x])
                    {
                        int plceHolder = nums[x];
                        nums[x] = nums[y];
                        nums[y] = plceHolder;
                        x--;
                        y--;
                    }
                    else
                    {
                        break;
                    }
                }     
            }
            return nums;
        }
    }
}
