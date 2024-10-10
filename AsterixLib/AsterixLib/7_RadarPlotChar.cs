using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class RadarPlotChar : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public RadarPlotChar(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }
        public override void Descodificar()
        {
            string SRL = base.info.Substring(0, 1);
            if (SRL == "0")
            {
                SRL = "Absence of Subfield #1";
            }
            else
            {
                SRL = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2));
            }

            string SRR = base.info.Substring(1, 1);
            if (SRR == "0")
            {
                SRR = "Absence of Subfield #2";
            }
            else
            {
                SRR = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2));
            }

            string SAM = base.info.Substring(2, 1);
            if (SAM == "0")
            {
                SAM = "Absence of Subfield #3";
            }
            else
            {
                SAM = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2)); // mal pot estar en complement a 2 --> ho he de fer, present esta
            }

            string PRL = base.info.Substring(3, 1);
            if (PRL == "0")
            {
                PRL = "Absence of Subfield #4";
            }
            else
            {
                PRL = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2));
            }

            string PAM = base.info.Substring(4, 1);
            if (PAM == "0")
            {
                PAM = "Absence of Subfield #5";
            }
            else
            {
                PAM = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2));
            }

            string RPD = base.info.Substring(5, 1);
            if (RPD == "0")
            {
                RPD = "Absence of Subfield #6";
            }
            else
            {
                RPD = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2)); // mal pot estar en complement a 2 --> ho he de fer, present esta
            }

            string APD = base.info.Substring(6, 1);
            if (APD == "0")
            {
                APD = "Absence of Subfield #7";
            }
            else
            {
                APD = Convert.ToString(Convert.ToInt32(base.info.Substring(9, 7), 2));
            }
        }
    }
}
