using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    class TargetReportDescriptor : DataItem
    {

        

        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TargetReportDescriptor(string category, int code, int length, string info)
            : base(category, code, info, length)
        {
            
        }


        // Implementación del método abstracto Descodificar
        string FX;
        public override void Descodificar()
        {
            //primero separaremos todos los valores y luego los decodificaremos
            string TYP = base.info.Substring(0, 3);
            switch (TYP)
            {
                case "000":
                    TYP = "No detection";
                    break;
                case "001":
                    TYP = "Single PSR detection";
                    break;
                case "010":
                    TYP = "Single SSR detection";
                    break;
                case "011":
                    TYP = "SSR + PSR detection";
                    break;
                case "100":
                    TYP = "Single ModeS All-Call";
                    break;
                case "101":
                    TYP = "Single ModeS Roll-Call";
                    break;
                case "110":
                    TYP = "ModeS All-Call + PSR";
                    break;
                case "111":
                    TYP = "ModeS Roll-Call +PSR";
                    break;
            }
         
            string SIM= base.info.Substring(3, 1);
            if (SIM == "0"){
                SIM = "Actual target report";
            }
            else
            {
                SIM = "Simulated target report";
            }
            string RDP= base.info.Substring(4, 1);
            if (RDP == "0")
            {
                RDP = "Report from RDP Chain 1";
            }
            else
            {
                RDP = "Report from RDP Chain 2";
            }
            string SPI= base.info.Substring(5, 1);
            if (SPI == "0")
            {
                SPI = "Absence of SPI";
            }
            else
            {
                SPI = "Special Position Identification";
            }
            string RAB= base.info.Substring(6, 1);
            if (RAB == "0")
            {
                RAB = "Report from aircraft transponder";
            }
            else
            {
                RAB = "Report from field monitor (fixed transponder)";
            }
            FX = base.info.Substring(7, 1);
            EscribirEnFichero(TYP + ";" + SIM + ";" + RDP + ";" + SPI + ";" + RAB + ";");
            if (FX == "1")
            {
                string TST = base.info.Substring(8, 1);
                if (TST == "0")
                {
                    TST = "Real target report";
                }
                else
                {
                    TST = "Test target report";
                }
                string ERR = base.info.Substring(9, 1);
                if (ERR == "0")
                {
                    ERR = "No Extended Range";
                }
                else
                {
                    ERR = "Extended Range present";
                }
                string XPP = base.info.Substring(10, 1);
                if (XPP == "0")
                {
                    XPP = "No X-Pulse present";
                }
                else
                {
                    XPP = "X-Pulse present";
                }
                string ME = base.info.Substring(11, 1);
                if (ME == "0")
                {
                    ME = "No military emergency";
                }
                else
                {
                    ME = "Military emergency";
                }
                string MI = base.info.Substring(12, 1);
                if (MI == "0")
                {
                    MI = "No military identification";
                }
                else
                {
                    MI = "Military identification";
                }
                string FOE = base.info.Substring(13, 2);
                switch (FOE)
                {
                    case "00":
                        FOE = "No Mode 4 interrogation";
                        break;
                    case "01":
                        FOE = "Friendly target";
                        break;
                    case "10":
                        FOE = "Unknown target";
                        break;
                    case "11":
                        FOE = "No reply";
                        break;
                }

                FX = base.info.Substring(15, 1);
                EscribirEnFichero(TST + ";" + ERR + ";" + XPP + ";" + ME + ";" + MI + ";" + FOE + ";");


                if (FX == "1")
                {
                    string ADSBEP = base.info.Substring(16, 1);
                    if (ADSBEP == "0")
                    {
                        ADSBEP = "ADSB not populated";
                    }
                    else
                    {
                        ADSBEP = "ADSB populated";
                    }
                    string ADSBVAL = base.info.Substring(17, 1);
                    if (ADSBVAL == "0")
                    {
                        ADSBVAL = "On-Site ADS-B Information not available";
                    }
                    else
                    {
                        ADSBVAL = "On-Site ADS-B Information available";
                    }
                    string SCNEP = base.info.Substring(18, 1);
                    if (SCNEP == "0")
                    {
                        SCNEP = "SCN not populated";
                    }
                    else
                    {
                        SCNEP = "SCN populated";
                    }
                    string SCNVAL = base.info.Substring(19, 1);
                    if (SCNVAL == "0")
                    {
                        SCNVAL = "Surveillance Cluster Network Information not available";
                    }
                    else
                    {
                        SCNVAL = "Surveillance Cluster Network Information available";
                    }
                    string PAIEP = base.info.Substring(20, 1);
                    if (PAIEP == "0")
                    {
                        PAIEP = "PAI not populated";
                    }
                    else
                    {
                        PAIEP = "PAI populated";
                    }
                    string PAIVAL = base.info.Substring(21, 1);
                    if (PAIVAL == "0")
                    {
                        PAIVAL = "Passive Acquisition Interface Information not available";
                    }
                    else
                    {
                        PAIVAL = "Passive Acquisition Interface Information available";
                    }
                    string SPARE = base.info.Substring(22, 1); //Siempre será 0
                    string FX = base.info.Substring(23, 1);
                    EscribirEnFichero(ADSBEP + ";" + ADSBVAL + ";" + SCNEP + ";" + SCNVAL + ";" + PAIEP + ";" + PAIVAL + ";");
                }
            }
        }
    }
}