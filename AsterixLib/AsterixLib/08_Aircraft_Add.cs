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
            string address = string.Empty;
            for (int i=0; i<base.info.Length; i+=4)
            {
                string bits = base.info.Substring(i, 4); //Agafem grups de 4 per a passar-ho a hexadecimal
                int decval = Convert.ToInt32(bits, 2); //Ho passem a decimal
                string address_char = decval.ToString("X");
                address += address_char;
            }

            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(address + ";", false);
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
