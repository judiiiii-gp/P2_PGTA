using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class ModeS : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS(string info)
            : base(info)
        {

        }

        

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            int REP = Convert.ToInt32(base.info.Substring(0, 8));
            for(int i = 0; i < REP; i++ )
            {
                string msg;
                int BDS1 = Convert.ToInt32(base.info.Substring(64, 4)); 
                int BDS2 = Convert.ToInt32(base.info.Substring(68, 4));

                if (BDS1 == 4 && BDS2 == 0)
                {
                    string MCP_FCUtxt;
                    int MCP_FCU = Convert.ToInt32(base.info.Substring(8, 1));
                    if (MCP_FCU == 1)
                    {
                        MCP_FCU = Convert.ToInt32(base.info.Substring(9, 12)) * 16;
                        MCP_FCUtxt = MCP_FCU.ToString();
                    }
                    else
                    {
                        MCP_FCUtxt = "N/A";
                    }

                    string FMStxt;
                    int FMS = Convert.ToInt32(base.info.Substring(21, 1));
                    if (FMS == 1)
                    {
                        FMS = Convert.ToInt32(base.info.Substring(22, 12)) * 16;
                        FMStxt = FMS.ToString();
                    }
                    else
                    {
                        FMStxt = "N/A";
                    }

                    string BARtxt;
                    int BAR = Convert.ToInt32(base.info.Substring(34, 1));
                    if (BAR == 1)
                    {
                        double BARdou = Convert.ToDouble(base.info.Substring(35, 12)) * 0.1;
                        BARtxt = BARdou.ToString();
                    }
                    else
                    {
                        BARtxt = "N/A";
                    }
                    int other = Convert.ToInt32(base.info.Substring(47, 16));

                    // Llamada al método EscribirEnFichero de la clase base
                    msg = MCP_FCUtxt + ";" + FMStxt + ";" + BARtxt + ";";
                    EscribirEnFichero(REP + ";" + msg + ";" + BDS1 + "," + BDS2);
                    //Debug.WriteLine("Hem escrit al fitxer");
                }
                else if (BDS1 == 5 && BDS2 == 0)
                {
                    //Debug.WriteLine("Estem al ModeS MB-5");
                    string Rolltxt;
                    int Roll = Convert.ToInt32(base.info.Substring(8, 1));
                    int SIGN_Roll = Convert.ToInt32(base.info.Substring(9, 1));// Left Wing Down
                    if (Roll == 1)
                    {
                        Roll = Convert.ToInt32(base.info.Substring(10, 9)) * (45 / 256);
                        Rolltxt = Roll.ToString();
                    }
                    else
                    {
                        Rolltxt = "N/A";
                    }

                    string TrueTracktxt;
                    int TrueTrack = Convert.ToInt32(base.info.Substring(19, 1));
                    int SIGN_TrueTrack = Convert.ToInt32(base.info.Substring(20, 1));// SIGN 1 = West (e.g. 315 = -45°) 
                    if (TrueTrack == 1)
                    {
                        TrueTrack = Convert.ToInt32(base.info.Substring(21, 10)) * (90 / 512);
                        TrueTracktxt = TrueTrack.ToString();
                    }
                    else
                    {
                        TrueTracktxt = "N/A";
                    }

                    string GroundSpeedtxt;
                    int GroundSpeed = Convert.ToInt32(base.info.Substring(31, 1));
                    if (GroundSpeed == 1)
                    {
                        GroundSpeed = Convert.ToInt32(base.info.Substring(32, 10)) * (1024 / 512);
                        GroundSpeedtxt = GroundSpeed.ToString();
                    }
                    else
                    {
                        GroundSpeedtxt = "N/A";
                    }

                    string TrackAngletxt;
                    int TrackAngle = Convert.ToInt32(base.info.Substring(42, 1));
                    int SIGN_TrackAngle = Convert.ToInt32(base.info.Substring(43, 1)); // SIGN 1 = Minus
                    if (TrackAngle == 1)
                    {
                        TrackAngle = Convert.ToInt32(base.info.Substring(44, 9)) * (6 / 256);
                        TrackAngletxt = TrackAngle.ToString();
                    }
                    else
                    {
                        TrackAngletxt = "N/A";
                    }

                    string TrueAirspeedtxt;
                    int TrueAirspeed = Convert.ToInt32(base.info.Substring(53, 1));
                    if (TrueAirspeed == 1)
                    {
                        TrueAirspeed = Convert.ToInt32(base.info.Substring(54, 10)) * (2);
                        TrueAirspeedtxt = TrueAirspeed.ToString();
                    }
                    else
                    {
                        TrueAirspeedtxt = "N/A";
                    }

                    // Llamada al método EscribirEnFichero de la clase base
                    msg = Rolltxt + ";" + TrueTracktxt + ";" + GroundSpeedtxt + ";" + TrackAngletxt + ";" + TrueAirspeedtxt + ";";
                    EscribirEnFichero(REP + ";" + msg + ";" + BDS1 + "," + BDS2);
                    //Debug.WriteLine("Hem escrit al fitxer");
                }
                else if (BDS1 == 6 && BDS2 == 0)
                {
                    //Debug.WriteLine("Estem al ModeS MB-6");
                    string MagHeadtxt;
                    int MagHead = Convert.ToInt32(base.info.Substring(8, 1));
                    int SIGN_MagHead = Convert.ToInt32(base.info.Substring(9, 1));// SIGN 1 = West (e.g. 315 = -45°) 
                    if (MagHead == 1)
                    {
                        MagHead = Convert.ToInt32(base.info.Substring(10, 10));
                        float MagHead_num = (float)MagHead * (90 / 512);
                        MagHeadtxt = MagHead_num.ToString();
                    }
                    else
                    {
                        MagHeadtxt = "N/A";
                    }

                    string IndAirtxt;
                    int IndAir = Convert.ToInt32(base.info.Substring(20, 1));
                    if (IndAir == 1)
                    {
                        IndAir = Convert.ToInt32(base.info.Substring(21, 10)) * 1;
                        IndAirtxt = IndAir.ToString();
                    }
                    else
                    {
                        IndAirtxt = "N/A";
                    }

                    string MACHtxt;
                    int MACH = Convert.ToInt32(base.info.Substring(31, 1));
                    if (MACH == 1)
                    {
                        double MACHdou = Convert.ToDouble(base.info.Substring(32, 10)) * (2.048 / 512);
                        MACHtxt = MACHdou.ToString();
                    }
                    else
                    {
                        MACHtxt = "N/A";
                    }

                    string BarAlttxt;
                    int BarAlt = Convert.ToInt32(base.info.Substring(42, 1));
                    int SIGN_BarAlt = Convert.ToInt32(base.info.Substring(43, 1));// SIGN 1 = Below
                    if (BarAlt == 1)
                    {
                        BarAlt = Convert.ToInt32(base.info.Substring(44, 9)) * 32;
                        BarAlttxt = BarAlt.ToString();
                    }
                    else
                    {
                        BarAlttxt = "N/A";
                    }

                    string InerVerttxt;
                    int InerVert = Convert.ToInt32(base.info.Substring(53, 1));
                    int SIGN_InerVert = Convert.ToInt32(base.info.Substring(54, 1));// SIGN 1 = Below
                    if (InerVert == 1)
                    {
                        InerVert = Convert.ToInt32(base.info.Substring(55, 9)) * 32;
                        InerVerttxt = InerVert.ToString();
                    }
                    else
                    {
                        InerVerttxt = "N/A";
                    }

                    // Llamada al método EscribirEnFichero de la clase base
                    msg = MagHeadtxt + ";" + IndAirtxt + ";" + MACHtxt + ";" + BarAlttxt + ";" + InerVerttxt + ";";
                    EscribirEnFichero(REP + ";" + msg + ";" + BDS1 + "," + BDS2);
                    //Debug.WriteLine("Hem escrit al fitxer");
                }
                else
                {
                    msg = "";
                    EscribirEnFichero(REP + ";" + msg + ";" + BDS1 + "," + BDS2);
                }
            }
        }
    }
}