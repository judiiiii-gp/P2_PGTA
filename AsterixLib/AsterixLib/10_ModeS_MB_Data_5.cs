using Microsoft.VisualBasic;
using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class ModeS5 : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public string Rolltxt {  get; private set; }
        public string TrueTracktxt { get; private set; }
        public string TrackAngletxt { get; private set; }
        public string GroundSpeedtxt { get; private set; }
        public string TrueAirspeedtxt {  get; private set; }
        public ModeS5(string info)
            : base(info)
        {

        }



        public override void Descodificar()
        {
            if (base.info == "N/A")
            {
                Rolltxt = "N/A";
                TrueTracktxt = "N/A";
                TrackAngletxt = "N/A";
                TrueAirspeedtxt = "N/A";
            }
            else
            {
                //Debug.WriteLine("Estem al ModeS MB-5");

                int Roll = Convert.ToInt32(base.info.Substring(0, 1));
                int SIGN_Roll = Convert.ToInt32(base.info.Substring(1, 1)); // SIGN 1 = Left Wing Down
                if (Roll == 1)
                {
                    string msg = base.info.Substring(2, 9);
                    if (SIGN_Roll == 1)
                    {
                        Roll = Convert.ToInt32(InvertirBits(msg)) * (45 / 256);
                    }
                    else
                    {
                        Roll = Convert.ToInt32(msg) * (45 / 256);
                    }
                    Rolltxt = Roll.ToString();
                }
                else
                {
                    Rolltxt = "N/A";
                }


                int TrueTrack = Convert.ToInt32(base.info.Substring(11, 1));
                int SIGN_TrueTrack = Convert.ToInt32(base.info.Substring(12, 1)); // SIGN 1 = West (e.g. 315 = -45°) 
                if (TrueTrack == 1)
                {
                    string msg = base.info.Substring(13, 10);
                    if (SIGN_TrueTrack == 1)
                    {
                        TrueTrack = Convert.ToInt32(InvertirBits(msg)) * (90 / 512);
                    }
                    else
                    {
                        TrueTrack = Convert.ToInt32(msg) * (90 / 512);
                    }

                    TrueTracktxt = TrueTrack.ToString();
                }
                else
                {
                    TrueTracktxt = "N/A";
                }


                int GroundSpeed = Convert.ToInt32(base.info.Substring(23, 1));
                if (GroundSpeed == 1)
                {
                    GroundSpeed = Convert.ToInt32(base.info.Substring(24, 10)) * (1024 / 512);
                    GroundSpeedtxt = GroundSpeed.ToString();
                }
                else
                {
                    GroundSpeedtxt = "N/A";
                }


                int TrackAngle = Convert.ToInt32(base.info.Substring(34, 1));
                int SIGN_TrackAngle = Convert.ToInt32(base.info.Substring(35, 1)); // SIGN 1 = Minus
                if (TrackAngle == 1)
                {
                    string msg = base.info.Substring(36, 9);
                    if (SIGN_TrackAngle == 1)
                    {
                        TrackAngle = Convert.ToInt32(InvertirBits(msg)) * (6 / 256);
                    }
                    else
                    {
                        TrackAngle = Convert.ToInt32(msg) * (6 / 256);
                    }

                    TrackAngletxt = TrackAngle.ToString();
                }
                else
                {
                    TrackAngletxt = "N/A";
                }


                int TrueAirspeed = Convert.ToInt32(base.info.Substring(45, 1));
                if (TrueAirspeed == 1)
                {
                    TrueAirspeed = Convert.ToInt32(base.info.Substring(46, 10)) * (2);
                    TrueAirspeedtxt = TrueAirspeed.ToString();
                }
                else
                {
                    TrueAirspeedtxt = "N/A";
                }
            }


   
            
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
        public override string ObtenerAtributos()
        {
            string mensaje = Rolltxt + ";" + TrueTracktxt + ";" + TrackAngletxt + ";" + TrueAirspeedtxt + ";";
            return mensaje;
        }
    }
}