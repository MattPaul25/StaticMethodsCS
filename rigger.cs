using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RigText
{


    public partial class Form1 : Form
    {
        public string[] txtLetters = {" ", "a", "b", "c", "d", "e", "f", "g", 
                                       "h", "i", "j", "k", "l", "m", "n", "o", 
                                       "p", "q", "r", "s", "t", "u", "v", "w",
                                       "x", "y", "z"};
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rigger();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dRigger();
        }

        private void Rigger()
        {
            string SomeText = textBox1.Text;

            for (int i = 0; i < SomeText.Length; i++)
            {
               string myChar = SomeText.Substring(i, 1);
               int newIndex = findIndex(myChar, txtLetters) + i;
               while (newIndex >= txtLetters.Length)
               {
                  newIndex = newIndex - txtLetters.Length;
               }
               myChar = txtLetters[newIndex];
               SomeText = ReplaceTextIndex(SomeText, myChar, i);
            }

            textBox1.Text = SomeText;
        }

        private void dRigger()
        {
            string SomeText = textBox1.Text;

            for (int i = 0; i < SomeText.Length; i++)
            {
                string myChar = SomeText.Substring(i, 1);
                int newIndex = findIndex(myChar, txtLetters) - i;
                while (newIndex < 0)
                {
                    newIndex = newIndex + txtLetters.Length;
                }
                myChar = txtLetters[newIndex];
                SomeText = ReplaceTextIndex(SomeText, myChar, i);
            }

            textBox1.Text = SomeText;
        }

        private static string ReplaceTextIndex(string yourString, string yourChar, int plcemnet)
        {
            int len = yourString.Length;
            string[] myStrngArr = SimpleSplit(yourString);
            myStrngArr[plcemnet] = yourChar;
            return convertArray(myStrngArr);
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
        
        private static string[] SimpleSplit(string aString)
        {
            string[] newArr = new string[aString.Length];

            for (int i = 0; i < aString.Length; i++)
            {
                newArr[i] = aString.Substring(i, 1);
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


    }
}
