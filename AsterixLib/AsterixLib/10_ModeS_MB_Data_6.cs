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
            //Debug.WriteLine("Estem al ModeS MB-6");
            string MagHeadtxt;
            int MagHead = Convert.ToInt32(base.info.Substring(0, 1));
            int SIGN_MagHead = Convert.ToInt32(base.info.Substring(1, 1)); // SIGN 1 = West (e.g. 315 = -45°) 
            if (MagHead == 1)
            {
                string msg = base.info.Substring(2, 10);    
                if (SIGN_MagHead == 1)
                {
                    MagHead = Convert.ToInt32(InvertirBits(msg)) * (90 / 512);
                    
                }
                else
                {
                    MagHead = Convert.ToInt32(msg) * (6 / 256);
                }
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
                IndAir = Convert.ToInt32(base.info.Substring(13, 9)) * 1;
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
                double MACHdou = Convert.ToDouble(base.info.Substring(24, 10)) * (2.048 / 512);
                MACHtxt = MACHdou.ToString();
            }
            else
            {
                MACHtxt = "N/A";
            }

            string BarAlttxt;
            int BarAlt = Convert.ToInt32(base.info.Substring(34, 1));
            int SIGN_BarAlt = Convert.ToInt32(base.info.Substring(35, 1)); // SIGN 1 = Below
            if (BarAlt == 1)
            {
                string msg = base.info.Substring(36, 9);
                if (SIGN_BarAlt == 1)
                {
                    BarAlt = Convert.ToInt32(InvertirBits(msg)) * 32;

                }
                else
                {
                    BarAlt = Convert.ToInt32(msg) * (6 / 256);
                }
                BarAlttxt = BarAlt.ToString();
            }
            else
            {
                BarAlttxt = "N/A";
            }

            string InerVerttxt;
            int InerVert = Convert.ToInt32(base.info.Substring(45, 1));
            int SIGN_InerVert = Convert.ToInt32(base.info.Substring(46, 1)); // SIGN 1 = Below
            if (InerVert == 1)
            {
                string msg = base.info.Substring(47, 9);
                if (SIGN_InerVert == 1)
                {
                    InerVert = Convert.ToInt32(InvertirBits(msg)) * 32;

                }
                else
                {
                    InerVert = Convert.ToInt32(msg) * (6 / 256);
                }
                InerVerttxt = InerVert.ToString();
            }
            else
            {
                InerVerttxt = "N/A";
            }
        }

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
