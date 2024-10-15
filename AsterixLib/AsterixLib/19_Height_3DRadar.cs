using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class H_3D_RADAR : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public H_3D_RADAR(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            string SPARE = base.info.Substring(0, 2); //Siempre seran 0

       
            string message = base.info.Substring(2);
            bool isNegative = message[0] == '1';
            int height;
            if (isNegative)
            {
               
                height = Convert.ToInt32(InvertirBits(message), 2) +1;
                height = -height; //Passem el valor a negatiu
            }
            else
            {
                height = Convert.ToInt32(message, 2); //El número està en positiu
            }



            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(height) + ";");
        }

        //Funció on invertim els bits per a fer el complement A2    

        public string InvertirBits(string message)
        {
            char[] bitsinvertidos = new char[message.Length];
            for (int i = 0; i<message.Length; i++)
            {
                bitsinvertidos[i] = message[i] == '0' ? '1' : '0'; //Invertim els bits
            }
            return new string (bitsinvertidos);
        }
    }
}