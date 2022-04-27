using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Uebung2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            String Kraftwerksnummer = TextBoxKraftwerksnummer.Text;
            String Unternehmen = TextBoxUnternehmen.Text;
            String Kraftwerksname = TextBoxKraftwerksname.Text;
            String PLZ = TextBoxPLZ.Text;
            String Ort = TextBoxOrt.Text;
            String Straße_Hausnummer = TextBoxStraßeHausnummer.Text;
            String Bundesland = TextBoxBundesland.Text;
            String Energieträger = TextBoxEnergieträger.Text;
            String Förderberechtigung_nach_EEG = TextBoxFörderberechtigung_nach_EEG.Text;
            String Netto_Nennleistung_MW = TextBoxNetto_Nennleistung_MW.Text;
            DateTime Beginn_Stromeinspeisung = DatePickerBeginnStromeinspeisung.SelectedDate.Value;

            Uebung02 uebung02 = new Uebung02(Kraftwerksnummer, Unternehmen, Kraftwerksname, PLZ, Ort, Straße_Hausnummer, Bundesland, Energieträger, Förderberechtigung_nach_EEG, Netto_Nennleistung_MW, Beginn_Stromeinspeisung);
            ListBoxShowInfo.Items.Add(uebung02);




        }

        private void ButtonLöschen_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxShowInfo.SelectedIndex > -1)
            {
                ListBoxShowInfo.Items.Remove(ListBoxShowInfo.SelectedItem);
            }
        }

        private void ButtonAlleLöschen_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShowInfo.Items.Clear();
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            TextBoxBundesland_Show.Text = "";
            TextBoxEnergieträger_Show.Text = "";
            TextBoxFörderberechtigung_nach_EEG_Show.Text = "";
            TextBoxKraftwerksname_Show.Text = "";
            TextBoxKraftwerksnummer_Show.Text = "";
            TextBoxNetto_Nennleistung_MW_Show.Text = "";
            TextBoxOrt_Show.Text = "";
            TextBoxPLZ_Show.Text = "";
            TextBoxStraßeHausnummer_Show.Text = "";
            TextBoxUnternehmen_Show.Text = "";
            DatePickerBeginnStromeinspeisung_Show.SelectedDate = new DateTime();


        }

        private void ButtonLadenCSV_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShowInfo.Items.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                List<Uebung02> eingeleseneUebung02 = Import.GetUebung02FromCSV(openFileDialog.FileName);

                eingeleseneUebung02.ForEach(uebung02 => ListBoxShowInfo.Items.Add(uebung02));
            }
        }

        private void ButtonLadenXLSX_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShowInfo.Items.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XLSX file (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                List<Uebung02> eingeleseneUbung02 = Import.GetUebung02FromXLSX(openFileDialog.FileName);
                eingeleseneUbung02.ForEach(uebung02 => ListBoxShowInfo.Items.Add(uebung02));
            }
        }

        private void ButtonExportXLSX_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XLSX file (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                List<Uebung02> alleUebung02 = new List<Uebung02>();

                foreach (Uebung02 uebung02 in ListBoxShowInfo.Items)
                {
                    alleUebung02.Add(uebung02);
                }
                
                Export.ExportAsXLSX(saveFileDialog.FileName, alleUebung02);
                
            }
        }

        private void ButtonAendern_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxShowInfo.SelectedIndex > -1)
            {
                Uebung02 uebung02 = (Uebung02)ListBoxShowInfo.SelectedItem;
                uebung02.Bundesland1 = TextBoxBundesland_Show.Text;
                uebung02.Energieträger1 = TextBoxEnergieträger_Show.Text;
                uebung02.Förderberechtigung_nach_EEG1 = TextBoxFörderberechtigung_nach_EEG_Show.Text;
                uebung02.Kraftwerksname1 = TextBoxKraftwerksname_Show.Text;
                uebung02.Kraftwerksnummer1 = TextBoxKraftwerksnummer_Show.Text;
                uebung02.Netto_Nennleistung_MW1 = TextBoxNetto_Nennleistung_MW_Show.Text;
                uebung02.Ort1 = TextBoxOrt_Show.Text;
                uebung02.PLZ1 = TextBoxPLZ_Show.Text;
                uebung02.Straße_Hausnummer1 = TextBoxStraßeHausnummer_Show.Text;
                uebung02.Unternehmen1 = TextBoxUnternehmen_Show.Text;
                uebung02.Beginn_Stromeinspeisung1 = DatePickerBeginnStromeinspeisung_Show.SelectedDate.Value;

                ListBoxShowInfo.Items.Refresh();
            }
        }

        private void ListBoxShowInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxShowInfo.SelectedIndex > -1)
            {
                Uebung02 uebung02 = (Uebung02)ListBoxShowInfo.SelectedItem;
                TextBoxBundesland_Show.Text =uebung02.Bundesland1;
                TextBoxEnergieträger_Show.Text = uebung02.Energieträger1;
                TextBoxFörderberechtigung_nach_EEG_Show.Text =uebung02.Förderberechtigung_nach_EEG1;
                TextBoxKraftwerksname_Show.Text =uebung02.Kraftwerksname1;
                TextBoxKraftwerksnummer_Show.Text =uebung02.Kraftwerksnummer1;
                TextBoxNetto_Nennleistung_MW_Show.Text =uebung02.Netto_Nennleistung_MW1;
                TextBoxOrt_Show.Text =uebung02.Ort1;
                TextBoxPLZ_Show.Text =uebung02.PLZ1;
                TextBoxStraßeHausnummer_Show.Text =uebung02.Straße_Hausnummer1;
                TextBoxUnternehmen_Show.Text =uebung02.Unternehmen1;
                DatePickerBeginnStromeinspeisung_Show.SelectedDate= uebung02.Beginn_Stromeinspeisung1;



            }
        }
    }
}
