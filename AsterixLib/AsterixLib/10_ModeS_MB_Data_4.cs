using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    class ModeS4 : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public ModeS4(string category, int code, string info,int length)
            : base(category, code, info, length)
        {

        }

        

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            string MCP_FCUtxt;
            int MCP_FCU = Convert.ToInt32(base.info.Substring(0, 1));
            if (MCP_FCU == 1)
            {
                MCP_FCU = Convert.ToInt32(base.info.Substring(1, 12));
                MCP_FCUtxt = MCP_FCU.ToString();
                // LSB --> 16 ft
            }
            else
            {
                MCP_FCUtxt = "N/A";
            }

            string FMStxt;
            int FMS = Convert.ToInt32(base.info.Substring(13, 1));
            if (FMS == 1)
            {
                FMS = Convert.ToInt32(base.info.Substring(14, 12));
                FMStxt = FMS.ToString();
                // LSB --> 16 ft
            }
            else
            {
                FMStxt = "N/A";
            }

            string BARtxt;
            int BAR = Convert.ToInt32(base.info.Substring(26, 1));
            if (BAR == 1)
            {
                BAR = Convert.ToInt32(base.info.Substring(27, 12));
                BARtxt = BAR.ToString();
                // LSB --> 0.1 mbar
            }
            else
            {
                BARtxt = "N/A";
            }

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(MCP_FCUtxt + ";" + FMStxt + ";" + BARtxt + ";");
        }
    }
}