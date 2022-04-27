using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uebung2
{
    public class Uebung02
    {
        private string Kraftwerksnummer;
        private string Unternehmen;
        private string Kraftwerksname;
        private string PLZ;
        private string Ort;
        private string Straße_Hausnummer;
        private string Bundesland;
        private string Energieträger;
        private string Förderberechtigung_nach_EEG;
        private string Netto_Nennleistung_MW;
        private DateTime Beginn_Stromeinspeisung;

        public Uebung02()
        {
            Kraftwerksnummer = "";
            Unternehmen = "";
            Kraftwerksname = "";
            PLZ = "";
            Ort = "";
            Straße_Hausnummer = "";
            Bundesland = "";
            Energieträger = "";
            Förderberechtigung_nach_EEG = "";
            Netto_Nennleistung_MW = "";
            Beginn_Stromeinspeisung = new DateTime();

        }

        public Uebung02(string Kraftwerksnummer, string Unternehmen, string Kraftwerksname, string PLZ, string Ort, string Straße_Hausnummer, string Bundesland, string Energieträger, string Förderberechtigung_nach_EEG, string Netto_Nennleistung_MW, DateTime Beginn_Stromeinspeisung)
        {
            this.Kraftwerksnummer = Kraftwerksnummer;
            this.Unternehmen = Unternehmen;
            this.Kraftwerksname = Kraftwerksname;
            this.PLZ = PLZ;
            this.Ort = Ort;
            this.Straße_Hausnummer = Straße_Hausnummer;
            this.Bundesland = Bundesland;
            this.Energieträger = Energieträger;
            this.Förderberechtigung_nach_EEG = Förderberechtigung_nach_EEG;
            this.Netto_Nennleistung_MW = Netto_Nennleistung_MW;
            this.Beginn_Stromeinspeisung = Beginn_Stromeinspeisung;
        }

        public string Kraftwerksnummer1 { get => Kraftwerksnummer; set => Kraftwerksnummer = value; }
        public string Unternehmen1 { get => Unternehmen; set => Unternehmen = value; }
        public string Kraftwerksname1 { get => Kraftwerksname; set => Kraftwerksname = value; }
        public string PLZ1 { get => PLZ; set => PLZ = value; }
        public string Ort1 { get => Ort; set => Ort = value; }
        public string Straße_Hausnummer1 { get => Straße_Hausnummer; set => Straße_Hausnummer = value; }
        public string Bundesland1 { get => Bundesland; set => Bundesland = value; }
        public string Energieträger1 { get => Energieträger; set => Energieträger = value; }
        public string Förderberechtigung_nach_EEG1 { get => Förderberechtigung_nach_EEG; set => Förderberechtigung_nach_EEG = value; }
        public string Netto_Nennleistung_MW1 { get => Netto_Nennleistung_MW; set => Netto_Nennleistung_MW = value; }
        public DateTime Beginn_Stromeinspeisung1 { get => Beginn_Stromeinspeisung; set => Beginn_Stromeinspeisung = value; }


        public override string ToString()
        {
            return Kraftwerksnummer1 + ", " + Unternehmen1 + ", " + Kraftwerksname1 + ", " + PLZ1 + ", " + Ort1 + ", " + Straße_Hausnummer1 + ", " + Bundesland1 + ", " + Energieträger1 + ", " + Förderberechtigung_nach_EEG1 + ", " + Netto_Nennleistung_MW1 + ", " + Beginn_Stromeinspeisung1;
        }
    }
}
