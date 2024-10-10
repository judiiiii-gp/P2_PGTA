using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class ModeS5 : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS5(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }

        

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            string Rolltxt;
            int Roll = Convert.ToInt32(base.info.Substring(0, 1));
            // SIGN 1 = Left Wing Down
            if (Roll == 1)
            {
                Roll = Convert.ToInt32(base.info.Substring(2, 9));
                Rolltxt = Roll.ToString();
                // LSB --> 45/256°
            }
            else
            {
                Rolltxt = "N/A";
            }

            string TrueTracktxt;
            int TrueTrack = Convert.ToInt32(base.info.Substring(11, 1));
            // SIGN 1 = West (e.g. 315 = -45°) 
            if (TrueTrack == 1)
            {
                TrueTrack = Convert.ToInt32(base.info.Substring(13, 10));
                TrueTracktxt = TrueTrack.ToString();
                // LSB --> = 90/512°
            }
            else
            {
                TrueTracktxt = "N/A";
            }

            string GroundSpeedtxt;
            int GroundSpeed = Convert.ToInt32(base.info.Substring(23, 1));
            if (GroundSpeed == 1)
            {
                GroundSpeed = Convert.ToInt32(base.info.Substring(24, 10));
                GroundSpeedtxt = GroundSpeed.ToString();
                // LSB --> 1 024/512 kt 
            }
            else
            {
                GroundSpeedtxt = "N/A";
            }

            string TrackAngletxt;
            int TrackAngle = Convert.ToInt32(base.info.Substring(34, 1));
            // SIGN 1 = Minus
            if (TrackAngle == 1)
            {
                TrackAngle = Convert.ToInt32(base.info.Substring(36, 9));
                TrackAngletxt = TrackAngle.ToString();
                // LSB --> 8/256°/s
            }
            else
            {
                TrackAngletxt = "N/A";
            }

            string TrueAirspeedtxt;
            int TrueAirspeed = Convert.ToInt32(base.info.Substring(45, 1));
            if (TrueAirspeed == 1)
            {
                TrueAirspeed = Convert.ToInt32(base.info.Substring(46, 10));
                TrueAirspeedtxt = TrueAirspeed.ToString();
                // LSB --> 2 kt
            }
            else
            {
                TrueAirspeedtxt = "N/A";
            }

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Rolltxt + ";" + TrueTracktxt + ";" + GroundSpeedtxt + ";" + TrackAngletxt + ";" + TrueAirspeedtxt + ";");
        }
    }
}