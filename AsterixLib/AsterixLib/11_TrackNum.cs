using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class TrackNum : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TrackNum(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }

        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            string TrackNum = base.info.Substring(0, 16);
            int TrackNumDecimal = Convert.ToInt32(TrackNum, 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(TrackNum) + ";");
        }
    }
}
