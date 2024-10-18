using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class AircraftAdd : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public AircraftAdd(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            //Debug.WriteLine("Estem al Aircraft Add");
            int aircraft = Convert.ToInt32(base.info.Substring(0, 8), 2);
            int address = Convert.ToInt32(base.info.Substring(8), 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(aircraft) + ";" + Convert.ToString(address) + ";");
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
