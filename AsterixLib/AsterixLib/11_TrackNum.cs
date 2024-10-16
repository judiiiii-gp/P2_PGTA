using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class TrackNum : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TrackNum(string info)
            : base(info)
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
