using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class AircraftID : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public AircraftID(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            length = 6;
            int[] posición;
            char[] characters;
            int i = 0;
            while (i<8)
            {
                posición[i]= Convert.ToInt32(base.info.Substring(length-length, length), 2);
                char[i]= Convert.ToChar(posición[i]);
                length = length + length;
                i++;
            }

            string aircraftID = new string(characters); //Convertimos los characteres en una secuencia

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(aircraftID + ";");
        }
    }
}
