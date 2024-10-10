using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class ModeS : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }

        

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            string MagHeadtxt;
            int MagHead = base.info.Substring(0, 1);
            // SIGN 1 = West (e.g. 315 = -45°) 
            if (MagHead == 1)
            {
                MagHead = Convert.ToInt32(base.info.Substring(2, 10));
                MagHeadtxt = MagHead.ToString();
                // LSB --> 90/512°
            }
            else
            {
                MagHeadtxt = 'N/A';
            }

            string IndAirtxt;
            int IndAir = base.info.Substring(12, 1);
            if (IndAir == 1)
            {
                IndAir = Convert.ToInt32(base.info.Substring(13, 9));
                IndAirtxt = IndAir.ToString();
                // LSB --> = 1 kt
            }
            else
            {
                IndAirtxt = 'N/A';
            }

            string MACHtxt;
            int MACH = base.info.Substring(23, 1);
            if (MACH == 1)
            {
                MACH = Convert.ToInt32(base.info.Substring(24, 10));
                MACHtxt = MACH.ToString();
                // LSB --> 2.048/512 MACH 
            }
            else
            {
                MACHtxt = 'N/A';
            }

            string BarAlttxt;
            int BarAlt = base.info.Substring(34, 1);
            // SIGN 1 = Below
            if (BarAlt == 1)
            {
                BarAlt = Convert.ToInt32(base.info.Substring(36, 9));
                BarAlttxt = BarAlt.ToString();
                // LSB --> 8 192/256 = 32 ft/min 
            }
            else
            {
                BarAlttxt = 'N/A';
            }

            string InerVerttxt;
            int InerVert = base.info.Substring(45, 1);
            // SIGN 1 = Below
            if (InerVert == 1)
            {
                InerVert = Convert.ToInt32(base.info.Substring(47, 9));
                InerVerttxt = InerVert.ToString();
                // LSB --> 8 192/256 = 32 ft/min
            }
            else
            {
                InerVerttxt = 'N/A';
            }

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Rolltxt + ";" + TrueTracktxt + ";" + GroundSpeedtxt + ";" + TrackAngletxt + ";" + TrueAirspeedtxt + ";");
        }
    }
}