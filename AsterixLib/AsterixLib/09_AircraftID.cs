using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class AircraftID : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public AircraftID(string info)
            : base(info)
        {

        }



       
        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al AircraftId");
            int length = 6;
            int pos = 0;
            int i = 0;
            int[] posición = new int[8];
            char[] characters = new char[8];
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
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
