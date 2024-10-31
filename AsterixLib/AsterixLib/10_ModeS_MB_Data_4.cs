using System;
using System.Diagnostics;
using System.Security.Authentication;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class ModeS4 : DataItem
    {
        public string MCP_FCUtxt {  get; private set; }
        public string FMStxt { get; private set; }
        public string BARtxt { get; private set; }
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS4(string info)
            : base(info)
        {

        }


        public override void Descodificar()
        {
            
            if (base.info == "N/A")
            {
                MCP_FCUtxt = "N/A";
                FMStxt = "N/A";
                BARtxt = "N/A";
            }
            else
            {
                //Debug.WriteLine("Estem al ModeS MB-4");

                long MCP_FCU = Convert.ToInt64(base.info.Substring(0, 1),2);
                if (MCP_FCU == 1)
                {
                    MCP_FCU = Convert.ToInt64(base.info.Substring(1, 12), 2) * 16;
                    MCP_FCUtxt = Convert.ToString(MCP_FCU);
                }
                else
                {
                    MCP_FCUtxt = "N/A";
                }


                long FMS = Convert.ToInt64(base.info.Substring(13, 1), 2);
                if (FMS == 1)
                {
                    FMS = Convert.ToInt64(base.info.Substring(14, 12), 2) * 16;
                    FMStxt = Convert.ToString(FMS);
                }
                else
                {
                    FMStxt = "N/A";
                }


                long BAR = Convert.ToInt64(base.info.Substring(26, 1), 2);
                if (BAR == 1)
                {
                    BAR = Convert.ToInt64(base.info.Substring(27, 12), 2);
                    double BARdou = ((BAR)/ 10) + 800;
                    if (BARdou > 1209 || BARdou < 800)
                    {
                        BARtxt = "NV";
                    }
                    else
                    {
                        BARtxt = Convert.ToString(BARdou);
                    }
                }
                else
                {
                    BARtxt = "N/A";
                }
            }
            



        }
        public override string ObtenerAtributos()
        {
            string mensaje = MCP_FCUtxt + ";" + FMStxt + ";" + BARtxt + ";";
            return mensaje;
        }
    }
}