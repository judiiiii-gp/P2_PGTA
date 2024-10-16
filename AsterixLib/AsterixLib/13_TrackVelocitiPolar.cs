using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class TrackVelocityPolar : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TrackVelocityPolar(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            int length = 16; //Cada octeto tiene 8 bits

            int groundspeed = Convert.ToInt32(base.info.Substring(0, length), 2);
            int heading = Convert.ToInt32(base.info.Substring(length), 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(groundspeed) + ";" + Convert.ToString(heading) + ";");
        }
    }
}