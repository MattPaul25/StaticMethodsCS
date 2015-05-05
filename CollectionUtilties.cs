using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;
 
 
 
 public static class CollectionUtils
    {
        public static DataTable ConvertArrList(ArrayList al)
        {           
            //works with array lists containing string arrays only            
            var arr = al.ToArray();
            var dt = new DataTable();                     
            var myArr = arr[0];
            string[] stringArr = myArr as string[];
            int i = 0;
            while (i < stringArr.Length)         
            {
                var columnName = stringArr[i];
                dt.Columns.Add(columnName);
                i++;
            }
            int j = 1;
            while (j < al.Count)
            {
                myArr = arr[j];
                stringArr = myArr as string[];
                dt.Rows.Add(stringArr);
                j++;
            }
            return dt;
        }
        public static DataTable ImportCSV(string fullPath, char sepChar = ',')
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(fullPath))
            {
                string firstLine = sr.ReadLine();
                var headers = firstLine.Split(sepChar);
                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }
                int columnInterval = headers.Count();
                string newLine = sr.ReadLine();
                while (newLine != null)
                {
                    var fields = newLine.Split(sepChar); // csv delimiter              
                    string[] adjustedFields = new string[columnInterval];
                    //want to bring in exactly the column interval amount of columns
                    for (int i = 0; i < columnInterval; i++)
                    {
                        adjustedFields[i] = fields[i];
                    }
                    dt.Rows.Add(adjustedFields);
                    newLine = sr.ReadLine();
                }
            }
            return dt;
        }
        public static string Vlookup(DataTable dt, string LookupVal, string LookupColumn, string ReturnColumn)
        {
            string result = "";
            foreach (DataRow row in dt.Rows)
            {
                if (row[LookupColumn].ToString() == LookupVal)
                {
                    result = row[ReturnColumn].ToString();
                    break;
                }
            }
            return result;
        }
        public static int GetIndex<T>(T Name, T[] Items)
        {
            int answer = -1;
            string myType = Name.GetType().Name;
            for (int i = 0; i < Items.Count(); i++)
            {
                if (myType == "String")
                {
                    if (Items[i].ToString().ToLower().Equals(Name.ToString().ToLower()))
                    {
                        answer = i;
                        break;
                    }
                }
                else
                {
                    if (Items[i].Equals(Name))
                    {
                        answer = i;
                        break;
                    }
                }
            }
            return answer;
        }
    }
