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
            int[] nums = { 3, 6, 3, 5, 5, 3, 4, 20, 4, 5, 3, 7, 6, 5, 4, 9, 100, 1, 24, 46, 6, 5, 6343, 32, 32, 54, 2, 4, 5, 42, 3, 43, 43, 78, 42, 34, 46 };
            nums = boolSort(nums);
            //nums = bubbleSort(nums);
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
            for (int z = 0; z < y; z++)
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
    }
}

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


    }
}
