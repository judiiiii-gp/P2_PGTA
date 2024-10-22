using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class CommACAS : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public CommACAS(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        string communication;
        string status;
        public override void Descodificar()
        {

            //Debug.WriteLine("Estem al com ACAS");
            int COM = Convert.ToInt32(base.info.Substring(0, 3), 2);
            //Debug.WriteLine("Hem agafat el COM");
            switch (COM)
            {
                case 0:
                    communication = "No communications capability";
                    break;
                case 1:
                    communication = "Comm. A and Comm. B capability";
                    break;
                case 2:
                    communication = "Comm. A, Comm. B and Uplink ELM";
                    break;
                case 3:
                    communication = "Comm. A, Comm. B, Uplink ELM and Downlink ELM";
                    break;
                case 4:
                    communication = "Level 5 Transponder capability";
                    break;
                case 5:
                    communication = "Not assigned";
                    break;
                case 6:
                    communication = "Not assigned";
                    break;
                case 7:
                    communication = "Not assigned";
                    break;

            }
            
            int STAT = Convert.ToInt32(base.info.Substring(3, 3), 2);
            //Debug.WriteLine("Hem agafat el STAT");
            switch (STAT)
            {
                case 0:
                    status = "No alert, no SPI, aircraft airborne";
                    break;
                case 1:
                    status = "No alert, no SPI, aircraft on ground";
                    break;
                case 2:
                    status = "Alert, no SPI, aircraft airborne";
                    break;
                case 3:
                    status = "Alert, no SPI, aircraft on ground";
                    break;
                case 4:
                    status = "Alert, SPI, aircraft airborne or on ground";
                    break;
                case 5:
                    status = "No alert, SPI, aircraft airborne or on ground";
                    break;
                case 6:
                    status = "Not assigned";
                    break;
                case 7:
                    status = "Unknown";
                    break;
            }
            string SI =base.info.Substring(6, 1);
            //Debug.WriteLine("Hem agafat el SI");
            if (SI == "0")
            {
                SI = "SI-Code Capable";
            }
            else
            {
                SI = "II-Code Capable";
            }

            string MSSC = base.info.Substring(8, 1); // El bit anterior es un spare bit
            //Debug.WriteLine("Hem agafat el MSSC");
            if (MSSC == "0")
            {
                MSSC = "No";
            }
            else
            {
                MSSC = "Yes";
            }
            string ARC =base.info.Substring(9, 1);
            //Debug.WriteLine("Hem agafat el ARC");
            if (ARC == "0")
            {
                ARC = "100 ft resolution";
            }
            else
            {
                ARC = "25 ft resolution";
            }
            string AIC = base.info.Substring(10, 1);
            //Debug.WriteLine("Hem agafat el AIC");
            if (AIC == "0")
            {
                AIC = "No";
            }
            else
            {
                AIC = "Yes";
            }
            int B1A = Convert.ToInt32(base.info.Substring(11, 1), 2);
            string B1A_mess = "BDS 1,0 bit 16=" + Convert.ToString(B1A);
            //Debug.WriteLine("Hem agafat el B1A_mess");
            int B1B = Convert.ToInt32(base.info.Substring(12, 4), 2);
            //Debug.WriteLine("Tenim el int");
            string B1B_mess = "BDS 1,0 bits 37/40="+ Convert.ToString(B1B);
            //Debug.WriteLine("Hem agafat el B1B_mess");
            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(communication + ";" + status + ";" + SI + ";" + MSSC + ";" + ARC + ";" + AIC + ";" + B1A_mess + ";" + B1B_mess + ";", false);
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
