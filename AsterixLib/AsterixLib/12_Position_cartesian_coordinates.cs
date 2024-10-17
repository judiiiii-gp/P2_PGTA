using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class Position_Cartesian : DataItem
    {

        


        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Position_Cartesian(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            Debug.WriteLine("Estem al Pos Cartes");
            int length = 16; //Cada octeto tiene 8 bits

            string x_coordinate = base.info.Substring(0, length);
           
             //Estan expressats amb el complement A2
            bool isNegative = x_coordinate[0] == '1';
            int X;
            int Y;
            if (isNegative)
            {

                X = Convert.ToInt32(InvertirBits(x_coordinate), 2) + 1;
                X = -X; //Passem el valor a negatiu
            }
            else
            {
                X = Convert.ToInt32(x_coordinate, 2); //El número està en positiu
            }
            
            string y_coordinate = base.info.Substring(length);
            isNegative = y_coordinate[0] == '1';
            if (isNegative)
            {

                Y = Convert.ToInt32(InvertirBits(y_coordinate), 2) + 1;
                Y = -Y; //Passem el valor a negatiu
            }
            else
            {
                Y = Convert.ToInt32(y_coordinate, 2); //El número està en positiu
            }


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(X) + ";" + Convert.ToString(Y) + ";");
            Debug.WriteLine("Hem escrit al fitxer");
        }
        public string InvertirBits(string message)
        {
            char[] bitsinvertidos = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                bitsinvertidos[i] = message[i] == '0' ? '1' : '0'; //Invertim els bits
            }
            return new string(bitsinvertidos);
        }
    }
}