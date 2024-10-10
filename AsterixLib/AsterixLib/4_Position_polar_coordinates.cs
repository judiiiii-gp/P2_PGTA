using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class Position_Polar : DataItem
    {

       


        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Position_Polar(string category, int code, int length, string info)
            : base(category, code, info, length)
        {
           

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            int length = 16; //Cada octeto tiene 8 bits

            string rho = base.info.Substring(0, length);
            string theta = base.info.Substring(length);
            // Convertir rho y theta de binario a decimal
            int Rho = Convert.ToInt32(rho, 2);
            int Theta = Convert.ToInt32(theta, 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(Rho) + ";" + convert.ToString(Theta) + ";");
        }
    }
}