using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class ModeS4 : DataItem
    {
        
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS4(string info)
            : base(info)
        {

        }


        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al ModeS MB-4");
            string MCP_FCUtxt;
            int MCP_FCU = Convert.ToInt32(base.info.Substring(0, 1));
            if (MCP_FCU == 1)
            {
                MCP_FCU = Convert.ToInt32(base.info.Substring(1, 12))*16;
                MCP_FCUtxt = MCP_FCU.ToString();
            }
            else
            {
                MCP_FCUtxt = "N/A";
            }

            string FMStxt;
            int FMS = Convert.ToInt32(base.info.Substring(13, 1));
            if (FMS == 1)
            {
                FMS = Convert.ToInt32(base.info.Substring(14, 12))*16;
                FMStxt = FMS.ToString();
            }
            else
            {
                FMStxt = "N/A";
            }

            string BARtxt;
            int BAR = Convert.ToInt32(base.info.Substring(26, 1));
            if (BAR == 1)
            {
                double BARdou = Convert.ToDouble(base.info.Substring(27, 12))*0.1;
                BARtxt = BARdou.ToString();
            }
            else
            {
                BARtxt = "N/A";
            }
        }
    }
}