using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{

    class Program
    {
        public static string[] CryptArr = { "Zs1", "Ys34", "Xds4", "Wwe54", "Vaf24", "35yr", "24d3", "Siou", "Re45", "Q", "Pillow", "O", "Nasty", "M", "L", "K", "J", "I", "H", "G", "F", "E", "D", "C", "B", "A", "$" };
        public static string[] AlphArr = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " " };
        static void Main(string[] args)
        {
            string myMessage = "shurid";
            string cryptMessage = encryptMessage(myMessage);
            Console.WriteLine(cryptMessage);
            string originalMessage = unEncryptMessage(cryptMessage);
            Console.WriteLine(originalMessage);
            Console.ReadLine();
        }

        private static string encryptMessage(string message)
        {
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
            string[,] charArray;
            
            int arraySize = 0;
            string newMessage = "";
            for (int i = 0; i < CryptArr.Length; i++)
            {
                //loop just gets the bounds of the array
                string MyChar = CryptArr[i];
                int strCount = countString(message, MyChar);
                if (strCount == 0) { continue; }
                arraySize = arraySize + strCount;
            }
            charArray = createArray(arraySize, message);
            string[] sortedCharArray = boolSort(arraySize, charArray);
            newMessage = string.Join("", sortedCharArray);
            return newMessage;
        }


        static string[] boolSort(int nLen, string[,] anArr)
        {
            string[] nArray = new string[nLen];

            for (int i = 0; i < nLen; i++)
            {
                int x = 0;
                int cnt = 0;
                while (x < nLen)
                {
                    int arrI = Convert.ToInt32(anArr[i, 1]);
                    int arrX = Convert.ToInt32(anArr[x, 1]);
                    if (arrI > arrX || (arrI == arrX && x > i))
                    { cnt++; }
                    x++;
                }
                nArray[cnt] = anArr[i ,0];
            }
            return nArray;
        }

        private static string[,] createArray(int arraySize, string message)
        {
            string[,] newArr = new string[arraySize, 2];
            int cnt = 0;
            for (int i = 0; i < CryptArr.Length; i++)
            {
                string MyChar = CryptArr[i];
                int strCount = countString(message, MyChar);
                if (strCount == 0) { continue; }
                int j = 1;
                while (j <= strCount)
                {
                    if (strCount != 0)                 
                    {                      
                        newArr[cnt, 0] = AlphArr[i];
                        newArr[cnt, 1] = Search(message, CryptArr[i], j, true).ToString();
                        j++;
                        cnt++;
                    }
                }                
            }
            return newArr;
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

        static int Search(string yourString, string yourMarker, int yourInst = 1, bool caseSensitive = true)
        {
            //returns the placement of a string in another string
            int num = 0;
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
        static int countString(string yourString, string yourMarker)
        {   
            int cnt = 0;
            int sLen = yourString.Length;
            int mLen = yourMarker.Length;
            for (int i = 0; i + mLen <= sLen; i++)
            {                
                if (yourString.Substring(i, mLen) == yourMarker)
                { 
                    cnt++; 
                }
            }
            return cnt;
        }
    }
}   
