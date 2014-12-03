using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Collections;
using System.Data.OleDb;


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
        private static string filePathY;
        private static string filePathT;
        private static string logDumpPath;
        private static string endPath; //path for moving files  - doesnt do anything while files arent being moved//

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
            filePathY = "V:\\Research\\Shared\\DailyOps\\Yesterday";
            filePathT = "V:\\Research\\Shared\\DailyOps\\Today";
            logDumpPath = "V:\\Research\\Shared\\DailyOps\\myFile.csv";
            endPath = "V:\\Research\\Shared\\DailyOps\\oldFiles\\";
            runThroughFiles();
            Console.WriteLine("Scipt Concluded");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Dana out..");
            System.Threading.Thread.Sleep(3000);
        }

       
        private static void runThroughFiles()
        {
            /*loops through files in directory and runs routines
              *outer most looping structure */
            string[] filNames = Directory.GetFiles(filePathT, "*.*", SearchOption.TopDirectoryOnly);
            string d = DateTime.Today.ToString("Mdyyyy");
            foreach (string filName in filNames)
            {
                for (int i = 0; i < 3; i++) { Console.WriteLine();}
                myTable = RightOf(filName, "\\");
                Console.WriteLine("working on " + myTable);
                string fileName2 = filePathY + "\\" + myTable;

                if (System.IO.File.Exists(fileName2))
                {
                    createRows(filName, '|'); //function gets the column names and assigns it to rows
                    editDateColumnNumber = GetColumnNumber("EditDate");
                    inputDateColumnNumber = GetColumnNumber("InputDate");
                    editByColumnNumber = GetColumnNumber("EditBy");
                    inputByColumnNumber = GetColumnNumber("InputBy");
                    ArrayList arrToday = grabCsvData(filName, "tblToday", '|', true);

                    if (arrToday.Count > 1)
                    {

                        int[] numbers = new[] { editDateColumnNumber, inputDateColumnNumber, editByColumnNumber, inputByColumnNumber };
                        var result = (from n in numbers where n < rows.Count() select n);

                        if (result.Count() < 4)
                        {
                            Console.WriteLine(myTable + "is missing edit or input headers");
                        }
                        else
                        {
                            ArrayList arrYesterday = grabCsvData(fileName2, "tblYesterday", '|', false);
                            ArrayList results = comparLists(arrToday, arrYesterday);
                            outputResults(results);
                            //FileInfo tFile = new FileInfo(fileName);
                            //FileInfo yFile = new FileInfo(fileName2);
                            //try
                            //{ //move le files to tomorrows position
                            //    yFile.MoveTo(endPath + d + myTable);
                            //    tFile.MoveTo(fileName2);
                            //}
                            //catch (Exception x) { Console.WriteLine(x.Message); }
                        }
                    }
                    else 
                    { 
                        Console.WriteLine("could not find edits or inputs for that date"); 
                    }
                }
                else
                {
                    Console.WriteLine("Path in yesterday file could not be found.. Going to next file");
                }
            }
        }
        private static ArrayList cleanDataTable(DataTable dt, bool today)
        {
            //filters out dates of todays table so it only compares a small number of records to yesterday
            string myOkDate1 = DateTime.Today.AddDays(-1).ToString("M/d/yyyy");
            string myOkDate2 = DateTime.Today.AddDays(-2).ToString("M/d/yyyy");
            int myLength = myOkDate1.Length;
            int myLength2 = myOkDate2.Length;
            ArrayList arList = new ArrayList();
            arList.Add(rows);
            if (today)
            {
               var query = from myRow in dt.AsEnumerable()
                            where
                            (myRow.Field<string>("InputDate") == myOkDate1 ||
                            myRow.Field<string>("InputDate") == myOkDate2)
                            || (myRow.Field<string>("EditDate") == myOkDate1 ||
                            myRow.Field<string>("EditDate") == myOkDate2)
                            select myRow;

                foreach (var item in query)
                {
                    string[] myStrnArr = new string[rows.Count()];
                    var myArr = item.ItemArray;
                    int x = 0;
                    foreach (var i in myArr)
                    {
                       myStrnArr[x] =  i.ToString();
                       x++;
                    }
                    arList.Add(myStrnArr);
                }
            }
            else if (!today)
            {
                var query = from myRow in dt.AsEnumerable() 
                            select myRow;
                 foreach (var item in query)
                {
                    string[] myStrnArr = new string[rows.Count()];
                    var myArr = item.ItemArray;
                    int x = 0;
                    foreach (var i in myArr)
                    {
                       myStrnArr[x] =  i.ToString();
                       x++;
                    }
                    arList.Add(myStrnArr);
                }
            }

             return arList;
        }        
        private static void outputResults(ArrayList results) 
        {
            //this method moves results to CSV file while also changes person name and adds team
            ArrayList alNames = pullNameData("V:\\Research\\Shared\\DailyOps\\NameMapping.xls");
            string team = "Team";
            string person = "";
            string[] ignore = { "EditBy", "InputBy", "EditDate", "InputDate", "EntityID", "RoundID" }; //field names to not include in log results
            Console.WriteLine("Dumping results -- here: " + logDumpPath);
            var csv = new StringBuilder();
            int i = System.IO.File.Exists(logDumpPath) ? 1: 0;
            if (i == 0) { csv.Append("Date|Researcher|Team|Change_Type|ID|Table|Field|From|To"); csv.Append(Environment.NewLine); i++; }
            
            while (i < results.Count)
            {
                object arrObj = results[i];
                string[] arrMyResults = arrObj as string[];
                int test = Array.IndexOf(ignore, arrMyResults[6]);
                if (Array.IndexOf(ignore, arrMyResults[4]) == -1)
                {
                    team = "";
                    person = arrMyResults[1];
                    for (int j = 0; j < alNames.Count; j++)
                    {
                        var objArr = alNames[j];
                        string[] myArr = objArr as string[];
                        if (myArr[2] == person || myArr[3] == person)
                        {
                            person = myArr[0];
                            team = myArr[1];
                            break;
                        }
                    }
                        csv.Append(arrMyResults[0] + "|" + person + "|" + team
                            + "|" + arrMyResults[3] + "|" + arrMyResults[4] + "|" + arrMyResults[5]
                            + "|" + arrMyResults[6] + "|" + arrMyResults[7] + "|" + arrMyResults[8]);
                        csv.Append(Environment.NewLine);
                }
                i++;
            }
            File.AppendAllText(logDumpPath, csv.ToString());
        }
        private static ArrayList pullNameData(string sourceFile)
        {
            string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + sourceFile + ";Extended Properties='Excel 8.0;HDR=Yes;'";
            string[] MyHeaders = { "Researcher", "Team", "ToolEamil", "Initials" };
            ArrayList myAl = new ArrayList();
            myAl.Add(MyHeaders);
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] objArr = { dr[0].ToString(), dr[5].ToString(), dr[2].ToString(), dr[3].ToString() };
                        myAl.Add(objArr);
                    }
                }
            }
            return myAl;
        }
        private static int GetColumnNumber(string ColumnName)
        {
            /*function gets the column number in array list that represents table from the first header row
              based on the column namespace */
            Console.WriteLine("figuring out " + ColumnName + " ordinal position for " + myTable);
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
            string[] MyHeaders = { "Date", "Researcher", "Team", "Change_Type", "ID", "Table", "Field", "From", "To" };
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
                        //value has been found now moving horizontally to find and record differences
                        int k = 1;
                        while (k < rows.Count())
                        {
                            if (myArr[k].ToLower().Trim() != myArr2[k].ToLower().Trim())
                            {
                                object objectField = arr[0];
                                string[] objectArr = objectField as string[];
                                string[] AddTo = { myArr[editDateColumnNumber], myArr[editByColumnNumber], "", "Edit", myArr[0], myTable, objectArr[k], myArr2[k], myArr[k] };
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
                            string[] AddTo = { myArr[inputDateColumnNumber], myArr[inputByColumnNumber], "", "Input", myArr[0], myTable, objectArr[n], "", myArr[n] };
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
        private static void createRows(string fileName, char splitChar)
        {
            //convert to function that just assigns value to field rows 
            Console.WriteLine("Adding " + myTable + " to program memory for further execution.. ");
            StreamReader sr = new StreamReader(fileName);
            string x = sr.ReadLine();
            ArrayList myList = new ArrayList();
            rows = x.Split(splitChar);
        }
        private static ArrayList grabCsvData(string fileName, string tableName, char splitChar, bool today)
        {
            Console.WriteLine("Adding " + myTable + " to program memory for further execution.. ");
            StreamReader sr = new StreamReader(fileName);
            string x = sr.ReadLine();
            DataTable dt = new DataTable();
            rows = x.Split(splitChar);
            foreach (string item in rows)
            {
                dt.Columns.Add(item);
            }
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
                if (currentArray.Length == rows.Length) { dt.Rows.Add(currentArray); }

                currentLine = sr.ReadLine();
                if (currentLine == null) { break; }
                currentArray = currentLine.Split(splitChar);
            }
            sr.Dispose();
            dt = cleanDates(dt, "InputDate");
            dt = cleanDates(dt, "EditDate");
            ArrayList al = cleanDataTable(dt, today);
            return al;
        }
        private static DataTable cleanDates(DataTable dt, string column)
        {
            //cleans dates of data table
            Console.WriteLine("Cleaning Dates of column " + column + " of " + myTable);
            foreach (DataRow row in dt.Rows)
            {
                string cellData = row[column].ToString();
                if (cellData != "")
                {
                    string newCell = LeftOf(cellData.ToString(), " ");
                    row[column] = newCell;
                }
            }
            return dt;
        }
        private static string LeftOf(string yourString, string yourMarker)
        {
            //method or function that pulls everything left of a unique Marker
            int anum = 0;
            int len = yourString.Length;
            int len2 = yourMarker.Length;
            string newString = "";
            do
            {
                string temp = yourString.Substring(anum, len2);
                if (temp == yourMarker)
                {
                    return newString;
                }
                newString = newString + temp;
                anum += 1;
            } while (anum < len);
            return "";
        }
        private static string RightOf(string yourString, string yourMarker)
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
    }
}
