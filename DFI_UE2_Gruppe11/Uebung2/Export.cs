using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Uebung2
{
    class Export
    {
        public static void ExportAsXLSX(String fileName, List<Uebung02> uebung02) //Excel-Tabellen exportieren
        {
            try
            {
                IWorkbook workbook = new XSSFWorkbook(); //Erstellen einer neuen Arbeitsmappe

                ICellStyle cellStyleRed = (ICellStyle)workbook.CreateCellStyle(); //Rendering-Verhalten rot
                cellStyleRed.FillForegroundColor = IndexedColors.Red.Index;
                cellStyleRed.FillPattern = FillPattern.SolidForeground;
                cellStyleRed.BorderBottom = BorderStyle.Thin;

                ICellStyle cellStyleBlue = (ICellStyle)workbook.CreateCellStyle(); //Rendering-Verhalten blau
                cellStyleBlue.FillForegroundColor = IndexedColors.LightBlue.Index;
                cellStyleBlue.FillPattern = FillPattern.SolidForeground;
                cellStyleBlue.BorderBottom = BorderStyle.Thin;

                //Alle Bundeslandnamen in der Tabelle
                string[] Bundslandlist = { "AWZ", "Baden-Wuerttemberg", "Bayern", "Berlin", "Brandenburg", "Bremen", "Bundesland", "Daenemark", "Hamburg", "Hessen", "Luxemburg", "Mecklenburg-Vorpommern", "Niedersachsen", "Nordrhein-Westfalen", "Oesterreich", "ohne Zuordnung", "Rheinland-Pfalz", "Saarland", "Sachsen", "Sachsen-Anhalt", "Schleswig-Holstein", "Schweiz", "Thueringen" };

                ISheet sheetUebersicht = workbook.CreateSheet("Übersicht");
                UebersichtErstellen(sheetUebersicht, uebung02);
                //Erstellen separate Tabellen für jeden Bundesland
                foreach(string tablename in Bundslandlist) { 
                    ISheet sheetKraftwerk = workbook.CreateSheet(tablename);
                    
                    List<Uebung02> newTable = SeparateTable(uebung02, tablename);
                    KraftwerklisteErstellen(sheetKraftwerk, newTable, cellStyleRed,cellStyleBlue);
                    DiagrammTop20(sheetKraftwerk, newTable);


                }
                
                

                FileStream fileStream = File.Create(fileName);
                workbook.Write(fileStream);
                fileStream.Close();

                TablleCount(workbook);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Der XLSX-Dateiexport ist fehlgeschlagen. \n" + exc);
            }
        }
        public static List<Uebung02> SeparateTable(List<Uebung02> uebung02,String tablename) //
        {
            //Aufgeteilt in verschiedene Tabellen basierend auf Staatsnamen
            List<Uebung02> newTable = new List<Uebung02>();
            for (int i = 0; i < uebung02.Count; i++)
            {
                Uebung02 h = uebung02[i];
                if (h.Bundesland1 == tablename)
                {
                    newTable.Add(h);
                }

            }
            return newTable;
        }


        public static void TablleCount(IWorkbook workbook)
        {
            //Wie viele Tabellen insgesamt in der exportierten Excel-Tabelle
            int num = workbook.NumberOfSheets;
            MessageBox.Show("XLSX-Daten werden erfolgreich generiert,\n es gibt insgesamt " + num + " Datentabellen");
        }

        public static void KraftwerklisteErstellen(ISheet sheet, List<Uebung02> uebung02, ICellStyle cellStyleRed, ICellStyle cellStyleBlue)
        {

            KraftwerklisteUeberschriften(sheet, cellStyleRed);
            KraftwerklisteWerte(sheet, uebung02, cellStyleRed, cellStyleBlue);
        }

        public static void KraftwerklisteUeberschriften(ISheet sheet, ICellStyle cellStyleRed)
        {
            //Überschriften für verschiedene Spalten
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Kraftwerksnummer1");
            row.CreateCell(1).SetCellValue("Unternehmen1");
            row.CreateCell(2).SetCellValue("Kraftwerksname1");
            row.CreateCell(3).SetCellValue("PLZ1");
            row.CreateCell(4).SetCellValue("Ort1");
            row.CreateCell(5).SetCellValue("Straße_Hausnummer1");
            row.CreateCell(6).SetCellValue("Bundesland1");
            row.CreateCell(7).SetCellValue("Energieträger1");
            row.CreateCell(8).SetCellValue("Förderberechtigung_nach_EEG1");
            row.CreateCell(9).SetCellValue("Netto_Nennleistung_MW1");
            row.CreateCell(10).SetCellValue("Beginn_Stromeinspeisung1");

            //Kopfzeile rot markiert
            row.Cells.ForEach(c => c.CellStyle = cellStyleRed);
        }

        public static void KraftwerklisteWerte(ISheet sheet, List<Uebung02> uebung02, ICellStyle cellStyleRed, ICellStyle cellStyleBlue)
        {
            //Order value from up to down
            uebung02 = uebung02.OrderByDescending(x => double.Parse(x.Netto_Nennleistung_MW1)).ToList();
            


            for (int i = 0; i < uebung02.Count; i++)
            {
                //Unterschiedliche Daten in unterschiedliche Spalten schreiben
                IRow row = sheet.CreateRow(i + 1);
                Uebung02 h = uebung02[i];
                row.CreateCell(0).SetCellValue(h.Kraftwerksnummer1);
                row.CreateCell(1).SetCellValue(h.Unternehmen1);
                row.CreateCell(2).SetCellValue(h.Kraftwerksname1);
                row.CreateCell(3).SetCellValue(h.PLZ1);
                row.CreateCell(4).SetCellValue(h.Ort1);
                row.CreateCell(5).SetCellValue(h.Straße_Hausnummer1);
                row.CreateCell(6).SetCellValue(h.Bundesland1);
                row.CreateCell(7).SetCellValue(h.Energieträger1);
                row.CreateCell(8).SetCellValue(bool.Parse(h.Förderberechtigung_nach_EEG1));
                row.CreateCell(9).SetCellValue(double.Parse(h.Netto_Nennleistung_MW1));
                row.CreateCell(10).SetCellValue(h.Beginn_Stromeinspeisung1.ToString("dd.MM.yyyy"));

                //Unterscheiden, in welcher Farbe jede Zeile gerendert wird
                if (bool.Parse(h.Förderberechtigung_nach_EEG1) == true)
                {
                    row.Cells.ForEach(c => c.CellStyle = cellStyleBlue);

                }
                else
                {
                    row.Cells.ForEach(c => c.CellStyle = cellStyleRed);
                }           
            }
           
        }
        public static double SumNNvalue(List<Uebung02> uebung02, int jahr)
        {
            //Aufteilung der Daten nach Jahr in verschiedene Gruppen
            List<Uebung02> Groupbyyear = new List<Uebung02>();
            for (int i = 0; i < uebung02.Count; i++)
            {
                Uebung02 h = uebung02[i];
                if (h.Beginn_Stromeinspeisung1.Year == jahr)
                {
                    Groupbyyear.Add(h);
                }

            }
            double Sumvalue = SumLeistungnachYear(Groupbyyear);
            return Sumvalue;
        }
        public static double SumLeistungnachYear(List<Uebung02> Groupbyyear)
        {
            //Statistische kumulative NN-Leistungswerte
            double h = 0;
            for (int i = 0; i < Groupbyyear.Count; i++)
            {
                try
                {
                    h = h + double.Parse(Groupbyyear[i].Netto_Nennleistung_MW1);
                }
                catch
                {
                }
            }
            return h;   
         
        }
         public static void DiagrammTop20(ISheet sheet, List<Uebung02> uebung02)
        {

            //Erstellung von Top20-Charts für jedes Bundesland
            IEnumerable<IGrouping<int, Uebung02>> uebung02Gruppiert = uebung02
                .OrderBy(h => h.Beginn_Stromeinspeisung1)
                .GroupBy(h => h.Beginn_Stromeinspeisung1.Year);
            int rowIndex = 1;
 

            int[] position = { 12, rowIndex, 22, rowIndex + 10 };
            int[] xWerte = { 1, 20, 2, 2 };
            int[] yWerte = { 1, 20, 9, 9 };

            DiagrammErstellen(sheet, position, xWerte, yWerte, "Top 20 der leistungsfähigsten Kraftwerke");
        }

        public static void UebersichtErstellen(ISheet sheet, List<Uebung02> uebung02)
        {
            //Eine Titelzeile erstellen
            UebersichtUeberschriften(sheet);

            //Unterschiedliche Gruppen je nach Jahr
            IEnumerable<IGrouping<int, Uebung02>> uebung02Gruppiert = uebung02
                .OrderBy(h => h.Beginn_Stromeinspeisung1)
                .GroupBy(h => h.Beginn_Stromeinspeisung1.Year);
            int rowIndex = 1;
            double anzahl = 0;

            //Der Gesamtwert wird berechnet und in die entsprechende Excel-Zelle geschrieben
            foreach (var gruppe in uebung02Gruppiert)
            {
               
                int jahr = gruppe.Key;

                anzahl = anzahl + SumNNvalue(uebung02, jahr);

                IRow row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(jahr);
                row.CreateCell(1).SetCellValue(anzahl);

              
                rowIndex++;
            }

            int[] position = { 12, rowIndex, 22, rowIndex + 10 };
            int[] xWerte = { 1, rowIndex, 0, 0 };
            int[] yWerte = { 1, rowIndex, 1, 1 };

            DiagrammErstellen(sheet, position, xWerte, yWerte, "Aggregierte Netto-Nennleistung");
        }

        public static void UebersichtUeberschriften(ISheet sheet)
        {
            //Datentitel für Aufgabe Teil 2
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Beginn_Stromeinspeisungjahr");
            row.CreateCell(1).SetCellValue("Aggregierte Netto-Nennleistung");
        }

        public static void DiagrammErstellen(ISheet sheet, int[] position, int[] xWerte, int[] yWerte,string name)
        {
            //Vorgehen vereinfacht nach https://github.com/nissl-lab/npoi/tree/master/ooxml/XSSF/UserModel/Charts
            //NPOI-Diagramme allgemein: https://poi.apache.org/apidocs/dev/org/apache/poi/ss/usermodel/charts/package-summary.html

            //Position
            IDrawing drawing = sheet.CreateDrawingPatriarch();
            IClientAnchor anchor = drawing.CreateAnchor(0, 0, 0, 0, position[0], position[1], position[2], position[3]);

            //Legende
            IChart chart = drawing.CreateChart(anchor);
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.TopRight;

            //Diagramm-Typ (Säulendiagramm)
            IBarChartData<double, double> data = chart.ChartDataFactory.CreateBarChartData<double, double>();

            //Achsen definieren und formatieren
            IChartAxis xAchse = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            IValueAxis yAchse = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            yAchse.Crosses = AxisCrosses.AutoZero;

            //Datenquelle angeben
            IChartDataSource<double> xs = DataSources.FromNumericCellRange(sheet, new CellRangeAddress(xWerte[0], xWerte[1], xWerte[2], xWerte[3]));
            IChartDataSource<double> ys = DataSources.FromNumericCellRange(sheet, new CellRangeAddress(yWerte[0], yWerte[1], yWerte[2], yWerte[3]));

            //Datenpunkte hinzufügen
            var serie = data.AddSeries(xs, ys);

            //Diagrammüberschrift setzen und Diagramm plotten
            serie.SetTitle(name);
            chart.Plot(data, xAchse, yAchse);
        }
    }
}
