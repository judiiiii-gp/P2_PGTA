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
        int[] posición;
        char[] characters;
       
        public override void Descodificar()
        {

            int length = 6;
            int pos = 0;
            int i = 0;
            while (i<8)
            {
                posición[i]= Convert.ToInt32(base.info.Substring(pos, length), 2);
                characters[i]= Convert.ToChar(posición[i]);
                pos = pos +length;
                i++;
            }

            string aircraftID = new string(characters); //Convertimos los characteres en una secuencia

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(aircraftID + ";");
        }
    }
}
