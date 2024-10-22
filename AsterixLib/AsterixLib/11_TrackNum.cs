using System;
using System.Diagnostics;

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
            //Debug.WriteLine("Estem al TrackNum");
            string spare = base.info.Substring(0, 4);
            string TrackNum = base.info.Substring(4, 12);
            int TrackNumDecimal = Convert.ToInt32(TrackNum, 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(TrackNumDecimal) + ";", false);
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}
