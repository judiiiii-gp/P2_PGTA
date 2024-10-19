using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class FlightLevel : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public FlightLevel(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al Flight Level");
            string V = base.info.Substring(0, 1);
            if (V == "0")
            {
                V = "Code validated";
            }
            else
            {
                V = "Code not validated";
            }
            string G = base.info.Substring(1, 1);
            if (G == "0")
            {
                G = "Default";
            }
            else
            {
                G = "Garbled code";
            }
                       
            int message = (Convert.ToInt32(base.info.Substring(2), 2));
            message = message / 4;


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(V + ";" + G + ";" + Convert.ToString(message) + ";");
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
