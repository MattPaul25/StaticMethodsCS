using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Collections;

namespace DailyOps
{
    class Program
    {
        private static int editByColumnNumber;
        private static int inputByColumnNumber;
        private static string[] rows;
        private static int editDateColumnNumber;
        private static int inputDateColumnNumber;
        private static string myTable;

        static void Main(string[] args)
        {
            /*program starts here - need to add the ability to store file locations that might change
             * perhaps a setup? */
            //add ability to check for trim changes 
            //check if dates are the same
            // add table name and team name of indiviudal with person mapping table

            DateTime d = DateTime.Today;
            Console.WriteLine("date " + d.ToString());
            Console.WriteLine("I'm Dana");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("..which is short for database analytics");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("so Da === Database");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("and ana === Analytics");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("....");
            System.Threading.Thread.Sleep(300);
            Console.WriteLine("...Just go with it...");
            Console.WriteLine("k.");
            Console.WriteLine("my programming allows me to search for tables in a given directory..");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("I check for changes in every field of two corresponding tables");
            Console.WriteLine(".");
            Console.WriteLine(".");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine(".");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine(".");
            Console.WriteLine("lets get started");
            string filePathY = "V:\\Research\\Shared\\DailyOps\\Yesterday";
            string filePathT = "V:\\Research\\Shared\\DailyOps\\Yesterday";
            string logDumpPath = "V:\\Research\\Shared\\DailyOps\\myFile.csv";
            string endPath = "V:\\Research\\Shared\\DailyOps\\oldFiles\\";
            runThroughFiles(filePathT, filePathY, endPath, logDumpPath);
            Console.WriteLine("Scipt Concluded");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Dana out..");
            System.Threading.Thread.Sleep(3000);
        }


        private static void runThroughFiles(string filePath1, string filePath2, string endPath, string log)
        {
          /*loops through files in directory and runs routines
            *outer most looping structure */
           string[] filNames = Directory.GetFiles(filePath1, "*.*", SearchOption.TopDirectoryOnly);
           string d = DateTime.Today.ToString("Mdyyyy");
           foreach (string fileName in filNames)
           {  
               myTable = RightOf(fileName, "\\");
               Console.WriteLine("working on " + myTable);
               string fileName2 = filePath2 + "\\" + myTable;

               if (System.IO.File.Exists(fileName2))
               {
                   ArrayList arr = grabCsvData(fileName, "tblToday", '|');
                   editDateColumnNumber = GetColumnNumber("EditDate");
                   inputDateColumnNumber = GetColumnNumber("InputDate");
                   editByColumnNumber = GetColumnNumber("EditBy");
                   inputByColumnNumber = GetColumnNumber("InputBy");
                   int[] numbers = new[] { editDateColumnNumber, inputDateColumnNumber, editByColumnNumber, inputByColumnNumber };
                   
                   var result = (from n in numbers where n < rows.Count() select n);

                   if (result.Count() < 4)
                   {
                       Console.WriteLine(myTable + "is missing edit or input headers");
                   }
                   else
                   {
                       arr = sortArrayList(arr);
                       ArrayList arr2 = grabCsvData(fileName2, "tblYesterday", '|');
                       ArrayList results = comparLists(arr, arr2);
                       outputResults(results, log);
                       FileInfo tFile = new FileInfo(fileName);
                       FileInfo yFile = new FileInfo(fileName2);
                       try
                       { //move le files to tomorrows position
                           yFile.MoveTo(endPath + d + myTable);
                           tFile.MoveTo(fileName2);
                       }
                       catch (Exception x) { Console.WriteLine(x.Message); }
                   }
               }
               else
               {
                   Console.WriteLine("Path in yesterday file could not be found.. Going to file 2");
               }
           }
        }
        static string RightOf(string yourString, string yourMarker)
        {
            //method or function that pulls everything right of a unique Marker
            int len = yourString.Length;
            int len2 = yourMarker.Length;
            int cnt = 0;
            for (int i = (len - len2); i > 0; i--)
            {
                cnt = cnt + 1;
                string temp = yourString.Substring(i, len2);
                if (temp == yourMarker)
                {
                    string newString = yourString.Substring(i + len2, cnt - 1);
                    return newString;
                }
            }
            return "";
        }
        private static void outputResults(ArrayList results, string filePath)
        {
            string[] ignore = { "EditBy", "InputBy", "EditDate", "InputDate" };
            Console.WriteLine("Dumping results -- here: " + filePath);
            var csv = new StringBuilder();
            int i = System.IO.File.Exists(filePath) ? 1: 0;
            while (i < results.Count)
            {
                object arrObj = results[i];
                string[] myResultsArr = arrObj as string[];
                int test = Array.IndexOf(ignore, myResultsArr[4]);
                if (Array.IndexOf(ignore, myResultsArr[4])==-1)
                {
                    csv.Append(myResultsArr[0] + "|" + myResultsArr[1] + "|" + myResultsArr[2]
                        + "|" + myResultsArr[3] + "|" + myResultsArr[4] + "|" + myResultsArr[5]
                        + "|" + myResultsArr[6] + "|" + myResultsArr[7]);
                    csv.Append(Environment.NewLine);
                }
                i++;
                
            }
            File.AppendAllText(filePath, csv.ToString());
        }
        private static ArrayList sortArrayList(ArrayList arr)
        {
            /*deletes the data that has not been edited yesterday or the day before to clear out issues
             */
            Console.WriteLine("sorting and removing old records from today file, this will be a bit");
            Console.WriteLine();
            string EditDateVal= "";
            string InputDateVal = "";
            ArrayList sortedArrLis = new ArrayList();
            sortedArrLis.Add(rows);
            int i = 1;
            int foundNum = 0;
            int lastRow = arr.Count;
            string myOkDate2 = DateTime.Today.AddDays(-2).ToString("M/d/yyyy");
            string myOkDate1 = DateTime.Today.AddDays(-1).ToString("M/d/yyyy");
            while (i < lastRow)
            {
                object arrObj = arr[i];
                string[] myArr = arrObj as string[];
                try { EditDateVal = Convert.ToDateTime(myArr[editDateColumnNumber]).ToString("M/d/yyyy");} catch {  EditDateVal = ""; }
                try { InputDateVal = Convert.ToDateTime(myArr[inputDateColumnNumber]).ToString("M/d/yyyy"); }  catch {  InputDateVal = ""; }
                    if (EditDateVal == myOkDate1 || EditDateVal == myOkDate2)
                    {
                        sortedArrLis.Add(myArr);
                        foundNum++;
                    }
                    else if (InputDateVal == myOkDate1 || InputDateVal == myOkDate2)
                    {
                        sortedArrLis.Add(myArr);
                        foundNum++;
                    }
                Console.Write("\r{0}/{1}  - found: {2}     ", i + 1, lastRow, foundNum);

                i++;
            }
            Console.WriteLine();
            return sortedArrLis;
         }
        private static int GetColumnNumber(string ColumnName)
        {
            /*function gets the column number in array list that represents table from the first header row
              based on the column namespace */
            Console.WriteLine("figuring out column ordinal positions");
            int i = 0;
            foreach (string col in rows)
            {
                if (rows[i] == ColumnName) 
                { 
                    return i; 
                }
                i++;
            }
            return i;
        }
        private static ArrayList comparLists(ArrayList arr, ArrayList arr2)
        { //this compares datasets by for each item in todays file in yesterday 
            //if it finds it - it looks for changes, otherwise it creates an input change type
            Console.WriteLine("comparing yesterdays file with todays...");
            ArrayList logFile = new ArrayList();
            string[] MyHeaders = { "Date","Researcher" ,"Team", "Change_Type", "Field", "ID", "From", "To" };
            string myDay = DateTime.Today.ToString();
            logFile.Add(MyHeaders);
            int myCount = arr.Count;
            int myCount2 = arr2.Count;
            int i = 1;
            while (i < myCount)
            {
                bool found = false;
                object arrList1 = arr[i];
                string[] myArr = arrList1 as string[];
                string myLookup = myArr[0];
                int j = 1;
                while (j < myCount2)
                {
                    object arrList2 = arr2[j];
                    string[] myArr2 = arrList2 as string[];
                    string myVal = myArr2[0];
                    if (myLookup == myVal) 
                    {
                        found = true;
                        //value has been found now moving laterally to record differences
                        int k = 1;
                        while (k < rows.Count())
                        {
                            if (myArr[k] != myArr2[k])
                            {
                                object objectField = arr[0];
                                string[] objectArr = objectField as string[];
                                string[] AddTo = { myDay, myArr[editByColumnNumber],"Test", "Edit" ,objectArr[k], myArr[0], myArr2[k], myArr[k] };
                                logFile.Add(AddTo);
                            }
                            k++;
                        }
                        break;
                    }
                    j++;
                }
                if (!found)
                { //handles the occassion where the id is not found so assumed to be an input
                    object objectField = arr[0];
                    string[] objectArr = objectField as string[];
                    int n = 0;
                    while (n < rows.Count())
                    {
                        if (myArr[n] != "")
                        {
                            string[] AddTo = { myDay, myArr[inputByColumnNumber], "Test", "Input", objectArr[n], myArr[0], "", myArr[n] };
                            logFile.Add(AddTo);
                        }
                        n++;
                    }
                }
                i++;
            }
            Console.WriteLine("End");
            return logFile;
        }

        private static ArrayList grabCsvData(string fileName, string tableName, char splitChar)
        {
            Console.WriteLine("Adding " + myTable + " to program memory for further execution.. ");
            StreamReader sr = new StreamReader(fileName);
            string x = sr.ReadLine();
            ArrayList myList = new ArrayList();
            rows = x.Split(splitChar);
            myList.Add(rows);
            int myInterval = rows.Count();
            string currentLine = sr.ReadLine();
            string[] currentArray = currentLine.Split(splitChar);

            while (currentArray != null)
            {
                int currentLength = currentArray.Length;
                if (currentLength != myInterval)
                {
                    while (currentLength < myInterval)
                    {
                        string newLine = sr.ReadLine();
                        currentLine = currentLine + newLine;
                        currentArray = currentLine.Split(splitChar);
                        currentLength = currentArray.Length;
                    }
                }
                myList.Add(currentArray);
                currentLine = sr.ReadLine();
                if (currentLine == null) { break; }
                currentArray = currentLine.Split(splitChar);
            }
            sr.Dispose();
            return myList;
        }
    }
}
