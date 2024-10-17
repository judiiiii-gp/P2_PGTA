using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class RadarPlotChar : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public RadarPlotChar(string info)
            : base(info)
        {

        }
        public override void Descodificar()
        {
            Debug.WriteLine("Estem al Radar Plot Chart");
            string SRL = base.info.Substring(0, 1);
            if (SRL == "0")
            {
                SRL = "Absence of Subfield #1";
            }
            else
            {
                SRL = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 8), 2)*(360/2^13));
                
            }

            string SRR = base.info.Substring(1, 1);
            if (SRR == "0")
            {
                SRR = "Absence of Subfield #2";
            }
            else
            {
                SRR = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 8), 2) * (1));
                
            }

            string SAM = base.info.Substring(2, 1);
            if (SAM == "0")
            {
                SAM = "Absence of Subfield #3";
                
            }
            else
            { 
                string message = base.info.Substring(9,8);
                bool isNegative = message[0] == '1';
                int SAMint;
                if (isNegative)
                {

                    SAMint = Convert.ToInt32(InvertirBits(message), 2) + 1; // si son dBms cal sumar-ho?
                    SAMint = -SAMint; //Passem el valor a negatiu
                }
                else
                {
                    SAMint = Convert.ToInt32(message, 2) + 1; // si son dBms cal sumar-ho?
                }
                SAM = Convert.ToString(SAMint);
                
            }

            string PRL = base.info.Substring(3, 1);
            if (PRL == "0")
            {
                PRL = "Absence of Subfield #4";
            }
            else
            {
                PRL = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 8), 2) * (360/2^13));
            }

            string PAM = base.info.Substring(4, 1);
            if (PAM == "0")
            {
                PAM = "Absence of Subfield #5";
            }
            else
            {
                string message = base.info.Substring(9, 8);
                bool isNegative = message[0] == '1';
                int PAMint;
                if (isNegative)
                {

                    PAMint = Convert.ToInt32(InvertirBits(message), 2) + 1; // si son dBms cal sumar-ho?
                    PAMint = -PAMint; //Passem el valor a negatiu
                }
                else
                {
                    PAMint = Convert.ToInt32(message, 2) + 1; // si son dBms cal sumar-ho?
                }
                PAM = Convert.ToString(PAMint);

            }

            string RPD = base.info.Substring(5, 1);
            if (RPD == "0")
            {
                RPD = "Absence of Subfield #6";
            }
            else
            {
                RPD = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2)*(1/256)); 
            }

            string APD = base.info.Substring(6, 1);
            if (APD == "0")
            {
                APD = "Absence of Subfield #7";
            }
            else
            {
                APD = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2)*(360/2^14));
            }
            EscribirEnFichero(SRL + ";" + SRR + ";" + SAM + ";" + PRL + ";" + PAM + ";" + RPD + ";" + APD + ";");
            Debug.WriteLine("Hem escrit al fitxer");
        }

        
        //Funció on invertim els bits per a fer el complement A2    
        public string InvertirBits(string message)
        {
            char[] bitsinvertidos = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                bitsinvertidos[i] = message[i] == '0' ? '1' : '0'; //Invertim els bits
            }
            return new string(bitsinvertidos);
        }
    }
}
