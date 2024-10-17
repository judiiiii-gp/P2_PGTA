using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class ModeS6 : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS6(string info)
            : base(info)
        {

        }

        

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            Debug.WriteLine("Estem al ModeS MB-6");
            string MagHeadtxt;
            int MagHead = Convert.ToInt32(base.info.Substring(0, 1));
            // SIGN 1 = West (e.g. 315 = -45°) 
            if (MagHead == 1)
            {
                MagHead = Convert.ToInt32(base.info.Substring(2, 10))*(90/512);
                MagHeadtxt = MagHead.ToString();
            }
            else
            {
                MagHeadtxt = "N/A";
            }

            string IndAirtxt;
            int IndAir = Convert.ToInt32(base.info.Substring(12, 1));
            if (IndAir == 1)
            {
                IndAir = Convert.ToInt32(base.info.Substring(13, 9))*1;
                IndAirtxt = IndAir.ToString();
            }
            else
            {
                IndAirtxt = "N/A";
            }

            string MACHtxt;
            int MACH = Convert.ToInt32(base.info.Substring(23, 1));
            if (MACH == 1)
            {   
                double MACHdou = Convert.ToDouble(base.info.Substring(24, 10))*(2.048/512);
                MACHtxt = MACHdou.ToString();
            }
            else
            {
                MACHtxt = "N/A";
            }

            string BarAlttxt;
            int BarAlt = Convert.ToInt32(base.info.Substring(34, 1));
            // SIGN 1 = Below
            if (BarAlt == 1)
            {
                BarAlt = Convert.ToInt32(base.info.Substring(36, 9))*32;
                BarAlttxt = BarAlt.ToString();
            }
            else
            {
                BarAlttxt = "N/A";
            }

            string InerVerttxt;
            int InerVert = Convert.ToInt32(base.info.Substring(45, 1));
            // SIGN 1 = Below
            if (InerVert == 1)
            {
                InerVert = Convert.ToInt32(base.info.Substring(47, 9))*32;
                InerVerttxt = InerVert.ToString();
            }
            else
            {
                InerVerttxt = "N/A";
            }

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(MagHeadtxt + ";" + IndAirtxt + ";" + MACHtxt + ";" + BarAlttxt + ";" + InerVerttxt + ";");
            Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}