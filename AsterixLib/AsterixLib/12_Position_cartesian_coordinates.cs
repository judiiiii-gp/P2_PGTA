using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class Position_Cartesian : DataItem
    {

        


        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Position_Cartesian(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            int length = 16; //Cada octeto tiene 8 bits

            string x_coordinate = base.info.Substring(0, length);
            string y_coordinate = base.info.Substring(length);
            // Convertir rho y theta de binario a decimal
            int X = Convert.ToInt32(rho, 2);
            int Y = Convert.ToInt32(theta, 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(X) + ";" + convert.ToString(Y) + ";");
        }
    }
}