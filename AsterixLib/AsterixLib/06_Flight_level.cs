using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class FlightLevel : DataItem
    {

        public string V {  get; private set; }
        public string G { get; private set; }
        public string FL { get; private set; }


        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public FlightLevel(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            if (base.info == "N/A")
            {
                V = "N/A";
                G = "N/A";
                FL = "N/A";
            }
            else
            {
                //Debug.WriteLine("Estem al Flight Level");
                V = base.info.Substring(0, 1);
                if (V == "0")
                {
                    V = "Code validated";
                }
                else
                {
                    V = "Code not validated";
                }
                G = base.info.Substring(1, 1);
                if (G == "0")
                {
                    G = "Default";
                }
                else
                {
                    G = "Garbled code";
                }

                int message = (Convert.ToInt32(base.info.Substring(2), 2));
                FL = Convert.ToString(message / 4);
            }
            //Debug.WriteLine("Hem escrit al fitxer");
        }
        public override string ObtenerAtributos()
        {
            string mensaje = V + ";" + G + ";"+ FL + ";";
            return mensaje;
        }
    }
}
