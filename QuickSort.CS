using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool test;
            int[] nums = { 4, 9, 1, 6, 3, 4, 2, 3, 8, 5 };
            //test = arraySorted(nums);
            QuickSort(nums);

            //for (int i = 0; i < nums.Length; i++)
            //{
            //    Console.WriteLine(nums[i].ToString());
            //}
            Console.ReadLine();
           
        }

        static int[] QuickSort(int[] nums)
        {
            int[] nums1;
            int[] nums2;
            int x;
            int currentIndex = nums.Length - 1;
            int piv = nums[currentIndex];
            int i = nums.Length - 2;
            int j = 0;
            int n = 0;
            while (i > j)
            {
                int lowIndex = nums[j];
                int highIndex = nums[i];

                if (lowIndex > piv && highIndex < piv)
                {
                    x = nums[j];
                    nums[j] = nums[i];
                    nums[i] = x;
                    i--;
                    j++;
                }
                else if (lowIndex > piv && highIndex > piv)
                {
                    i--;
                }
                else if (lowIndex < piv && highIndex < piv)
                {
                    j++;
                }
                else
                {
                    i--;
                    j++;
                }
                n++;
            }

            if (nums[++j] > nums[currentIndex])
            {
                x = nums[j];
                nums[j] = nums[currentIndex];
                nums[currentIndex] = x;
            }

            nums1 = PartitionArray(nums, 0, j);
            nums2 = PartitionArray(nums, j, currentIndex);

            if (arraySorted(nums1))
            {
                WriteOutNumbers(nums1);
            }
            else
            {
                QuickSort(nums1);
            }

            if (arraySorted(nums2))
            {
                WriteOutNumbers(nums2);
            }
            else
            {
                QuickSort(nums2);
            }
            return nums;

        }

        static int[] PartitionArray(int[] nums, int startLocation, int endLocation)
        {
            int i;
            int addFactor = (startLocation == 0) ? 0 : 1;
            int x = endLocation - startLocation + addFactor;
            int[] newArr = new int[x];
            for (i = 0; i < x; i++)
            {
                newArr[i] = nums[startLocation];
                startLocation++;
            }
            return newArr;
        }

        static bool arraySorted(int[] nums)
        {
            bool ret = true;
            int x = 0;
            int y = 1;
            while (y < nums.Length)
            {
                if (nums[x] > nums[y])
                {
                    ret = false;
                    break;
                }
                y++;
                x++;
            }
            return ret;
        }
        static void WriteOutNumbers(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i].ToString());
            }
        }
    }
           
}
