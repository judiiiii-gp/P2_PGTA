using System;
using System.Data.SqlTypes;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    class TrackStatus : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TrackStatus(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            string CNF = base.info.Substring(0, 1);
            if (CNF == "0")
            {
                CNF = "Confirmed Track";
            }
            else
            {
                CNF = "Tentative Track";
            }

            string RAD = base.info.Substring(1, 2);
            switch (RAD)
            {
                case "00":
                    RAD = "Combined Track";
                    break;
                case "01":
                    RAD = "PSR Track";
                    break;
                case "10":
                    RAD = "SSR/Mode S Track";
                    break;
                case "11":
                    RAD = "Invalid";
                    break;
            }

            string DOU = base.info.Substring(3, 1);
            if (DOU == "0")
            {
                DOU = "Normal confidence";
            }
            else
            {
                DOU = "Low confidence in plot to track association.";
            }

            string MAH = base.info.Substring(4, 1);
            if (MAH == "0")
            {
                MAH = "No horizontal man.sensed";
            }
            else
            {
                MAH = "Horizontal man.sensed";
            }

            string CDM = base.info.Substring(5, 2);
            switch (CDM)
            {
                case "00":
                    CDM = "Maintaining";
                    break;
                case "01":
                    CDM = "Climbing";
                    break;
                case "10":
                    CDM = "Descending";
                    break;
                case "11":
                    CDM = "Unknown";
                    break;
            }

            string FX = base.info.Substring(7, 1);
            EscribirEnFichero(CNF + ";" + RAD + ";" + DOU + ";" + MAH + ";" + CDM + ";");
            if (FX == "1")
            {
                string TRE = base.info.Substring(8, 1);
                if (TRE == "0")
                {
                    TRE = "Track still alive";
                }
                else
                {
                    TRE = "End of track lifetime";
                }
                string GHO = base.info.Substring(9, 1);
                if (GHO == "0")
                {
                    GHO = "True target track";
                }
                else
                {
                    GHO = "Ghost target track";
                }
                string SUP = base.info.Substring(10, 1);
                if (SUP == "0")
                {
                    SUP = "NO";
                }
                else
                {
                    SUP = "YES";
                }
                string TCC = base.info.Substring(11, 1);
                if (TCC == "0")
                {
                    TCC = "Tracking performed in so-called Radar Plane";
                }
                else
                {
                    TCC = "Slant range correction and a suitable projection technique are used to track in a 2D";
                }
                string Spare = base.info.Substring(12, 4);
                EscribirEnFichero(TRE + ";" + GHO + ";" + SUP + ";" + TCC);
            }
        }
    }
}