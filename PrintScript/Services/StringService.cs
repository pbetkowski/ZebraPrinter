using PrintScript.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace PrintScript.Services
{
    public class StringService
    {   

        private static StringService instance = null;

        public string ReadFile()
        {

            string ZPLString = "";

            try
            {   
                using (StreamReader sr = new StreamReader(@"C:\Dokumentacja\etykietaEDI.txt"))
                {
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

        public string ConvertZplFile(string zplFile, Label label)
        {
            try
            {
                StringBuilder sb = new StringBuilder(zplFile);
                sb.Replace("StringPar1", label.Odbiorca);
                sb.Replace("StringPar2", label.AdviceNote);
                sb.Replace("StringPar3", label.PartNo);
                sb.Replace("StringPar4", label.Quantity.ToString());
                sb.Replace("StringPar5", label.Description);
                sb.Replace("StringPar6", label.Supplier);
                sb.Replace("StringPar7", label.SupplierPartNumber);
                sb.Replace("StringPar8", label.SupplierPartNumber); // dopytać agatę
                sb.Replace("StringPar9", label.Street);
                sb.Replace("NumericPar1", label.GTL);
                sb.Replace("NumericPar2", label.Gate);
                sb.Replace("NumericPar4", label.LabelNo.ToString());
                Console.WriteLine(label.LabelNo.ToString());


                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return zplFile;
            }
        }

        private StringService() { }

        public static  StringService GetInstance()
        {
            if (instance == null)
            {
                instance = new StringService();
            }

            return instance;
        }
    }
}
