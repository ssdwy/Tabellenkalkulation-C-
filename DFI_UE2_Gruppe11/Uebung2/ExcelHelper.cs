using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uebung2


{
    public class ExcelHelper
    {
        public static DateTime GettCellValueDate(ISheet sheet, int row, int column)
        {
            //Datum lesen
            //Falls kein Datum angegeben: 01.01.1990
            DateTime date = new DateTime(1990, 01, 01);

            if (sheet.GetRow(row).GetCell(column,MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString() != "")
            {
                date = sheet.GetRow(row).GetCell(column, MissingCellPolicy.CREATE_NULL_AS_BLANK).DateCellValue;
            }

          

            return date;
        }

        public static DateTime GettCellValueDateCSV(String[] datum,int i)
        {
            //Datum lesen
            //Falls kein Datum angegeben: 01.01.1990
            DateTime date = new DateTime(1990, 01, 01);

            if (i < 2170)
            {
                int jahr = int.Parse(datum[2]);
                int tag = int.Parse(datum[0]);
                int monat = int.Parse(datum[1]);

                date = new DateTime(jahr, monat, tag);
            }



            return date;
        }

        
        public static string GettCellValueDouble(ISheet sheet, int row, int column)
        {
            //Lesen von Daten vom Typ Double
            //Hier: 0 als default-Wert
            return sheet.GetRow(row).GetCell(column, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
        }

        public static string GettCellValueString(ISheet sheet, int row, int column)
        {
            //Lesen von Daten vom Typ String
            //Hier:"" als default-Wert

            String Textcontent = "";

            if (sheet.GetRow(row).GetCell(column, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString() != "")
            {
                Textcontent = sheet.GetRow(row).GetCell(column, MissingCellPolicy.CREATE_NULL_AS_BLANK).StringCellValue;
            }
            return Textcontent;
        }

        public static string GettCellValueBool(ISheet sheet, int row, int column)
        {
            //Lesen von Daten vom Typ Bool
            //Hier: true als default-Wert

            String Boolvalue = "true";

            if (sheet.GetRow(row).GetCell(column, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString() != "Ja")
            {
                Boolvalue = "false";
            }
            return Boolvalue;
        }

        public static string GettCellValueBoolCSV(string Boolwert)
        {
            //Konvertierung der ursprünglichen Excel-Bool-Daten
            String Boolvalue = "true";

            if (Boolwert != "Ja")
            {
                Boolvalue = "false";
            }
            return Boolvalue;
        }
    }
}
