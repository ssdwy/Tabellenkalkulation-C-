using Microsoft.VisualBasic.FileIO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Uebung2

{
    public class Import
    {
        public static List<Uebung02> GetUebung02FromCSV(String path)
        {
            List<Uebung02> uebung02 = new List<Uebung02>();

            try
            {
                using (TextFieldParser parser = new TextFieldParser(path))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    parser.ReadLine();
                    parser.ReadLine();
                    parser.ReadLine();
                    parser.ReadLine();
                    parser.ReadLine();
                    parser.ReadLine();//Spaltenköpfe überspringen
                    int i = 1;

                    while (!parser.EndOfData)
                    {
                        //Entsprechende Spaltendaten aus .csv lesen
                        string[] fields = parser.ReadFields(); 
                        String Kraftwerksnummer = fields[0];
                        String Unternehmen = fields[1];
                        String Kraftwerksname = fields[2];
                        String PLZ = fields[3];
                        String Ort = fields[4];
                        String Straße_Hausnummer = fields[5];
                        String Bundesland = fields[6];
                        String Energieträger = fields[10];
                        String Förderberechtigung_nach_EEG = ExcelHelper.GettCellValueBoolCSV(fields[14]);
                        String Netto_Nennleistung_MW = fields[16];

                        String[] datum = fields[8].Split('.');
                        DateTime Beginn_Stromeinspeisung = ExcelHelper.GettCellValueDateCSV(datum,i);
                        i = i + 1;
                        Console.WriteLine(i);
   
                        uebung02.Add(new Uebung02(Kraftwerksnummer, Unternehmen, Kraftwerksname, PLZ, Ort, Straße_Hausnummer, Bundesland, Energieträger, Förderberechtigung_nach_EEG, Netto_Nennleistung_MW, Beginn_Stromeinspeisung));
                    }
                }


            }
            catch (Exception exc)
            {   
                MessageBox.Show("Der CSV-Dateiimport ist fehlgeschlagen. \n" + exc);
                return new List<Uebung02>();
            }


            return uebung02;
        }

        public static List<Uebung02> GetUebung02FromXLSX(String path)
        {
            List<Uebung02> uebung02 = new List<Uebung02>();

            try
            {
                IWorkbook workbook = new XSSFWorkbook(path);

                ISheet sheet = workbook.GetSheetAt(0);
                int rowIndex = 1;
                //Entsprechende Spaltendaten aus .xlsx lesen
                while (sheet.GetRow(rowIndex) != null)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    String Kraftwerksnummer = ExcelHelper.GettCellValueString(sheet, rowIndex, 0);
                    String Unternehmen = ExcelHelper.GettCellValueString(sheet, rowIndex, 1);
                    String Kraftwerksname = ExcelHelper.GettCellValueString(sheet, rowIndex, 2);
                    String PLZ = ExcelHelper.GettCellValueString(sheet, rowIndex, 3);
                    String Ort = ExcelHelper.GettCellValueString(sheet, rowIndex, 4);
                    String Straße_Hausnummer = ExcelHelper.GettCellValueString(sheet, rowIndex, 5);
                    String Bundesland = ExcelHelper.GettCellValueString(sheet, rowIndex, 6);
                    String Energieträger = ExcelHelper.GettCellValueString(sheet, rowIndex, 10);
                    String Förderberechtigung_nach_EEG = ExcelHelper.GettCellValueBool(sheet, rowIndex, 14);
                    String Netto_Nennleistung_MW = ExcelHelper.GettCellValueDouble(sheet, rowIndex, 16);

                    DateTime Beginn_Stromeinspeisung =ExcelHelper.GettCellValueDate(sheet, rowIndex, 8); 

                    Uebung02 h = new Uebung02(Kraftwerksnummer, Unternehmen, Kraftwerksname, PLZ, Ort, Straße_Hausnummer, Bundesland, Energieträger, Förderberechtigung_nach_EEG, Netto_Nennleistung_MW, Beginn_Stromeinspeisung);
                    uebung02.Add(h);

                    rowIndex++;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Der XLSX-Dateiimport ist fehlgeschlagen. \n" + exc);
                return new List<Uebung02>();
            }

            return uebung02;
        }

    }
}
