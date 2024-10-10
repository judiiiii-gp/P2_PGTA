using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class H_3D_RADAR : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public H_3D_RADAR(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            string SPARE = base.info.Substring(0, 2); //Siempre seran 0
       
            int message = Convert.ToInt32(base.info.Substring(2), 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(message) + ";");
        }
    }
}