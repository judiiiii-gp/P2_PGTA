using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class AircraftAdd : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public AircraftAdd(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
           

            int aircraft = Convert.ToInt32(base.info.Substring(0, 8), 2);
            int address = Convert.ToInt32(base.info.Substring(8), 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(aircraft) + ";" + Convert.ToString(address) + ";");
        }
    }
}
