using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class Position_Polar : DataItem
    {

       


        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Position_Polar(string info)
            : base( info)
        {
           

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al Position Polar");
            int length = 16; //Cada octeto tiene 8 bits

            string rho = base.info.Substring(0, length);
            string theta = base.info.Substring(length);
            // Convertir rho y theta de binario a decimal
            int Rho = Convert.ToInt32(rho, 2);
            int Theta = Convert.ToInt32(theta, 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(Rho) + ";" + Convert.ToString(Theta) + ";");
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}