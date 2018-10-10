using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace PrintScript.Services
{
    public class StringService
    {
        public string ConvertZplFile(string zplFile, List<string> list)
        {
            try
            {
                StringBuilder sb = new StringBuilder(zplFile);
                sb.Replace("StringPar1", list[0]);
                sb.Replace("StringPar2", list[1]);
                sb.Replace("StringPar3", list[2]);
                sb.Replace("StringPar4", list[3]);
                sb.Replace("StringPar5", list[4]);
                sb.Replace("StringPar6", list[5]);
                sb.Replace("StringPar7", list[6]);
                sb.Replace("StringPar8", list[7]);
                sb.Replace("StringPar9", list[8]);
                sb.Replace("NumericPar1", list[9]);
                sb.Replace("NumericPar2", list[10]);
                sb.Replace("NumericPar4", list[11]);


                return sb.ToString();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return zplFile;
            }
        }

        public string ReadFile()
        {

            string ZPLString = "";

            try
            {   
                using (StreamReader sr = new StreamReader(@"C:\Dokumentacja\etykietaEDI.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    ZPLString = sr.ReadToEnd();
                    return ZPLString;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }
    }
}
