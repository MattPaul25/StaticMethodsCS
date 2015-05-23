using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;


namespace Utilties
{
 class ErrorUtilties
    {
        public void LogError(string Path, string ErrMessage)
        {
            using (StreamWriter sr = new StreamWriter(Path, true))
            {
                sr.WriteLine(DateTime.Now.ToString());
                sr.WriteLine(ErrMessage);
                sr.Close();
            }
        }
        public void LogError(string Path, Exception x)
        {
            using (StreamWriter sr = new StreamWriter(Path, true))
            {
                sr.WriteLine(DateTime.Now.ToString());
                sr.WriteLine(x.Message);
                if (x.InnerException.Message != null)
                {
                    sr.WriteLine(x.InnerException.Message);
                }     
                sr.Close();
            }
        }
}
