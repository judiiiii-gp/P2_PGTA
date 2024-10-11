using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    class TimeOfDay : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TimeOfDay(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }
        public override void Descodificar()
        {

            int length = 8; //Cada octeto tiene 8 bits

            string hour = base.info.Substring(0, length);
            string min = base.info.Substring(8,  length);
            string sec = base.info.Substring(16, length);
            // Convertir de binario a decimal
            int hourDecimal = Convert.ToInt32(hour, 2);
            int minDecimal = Convert.ToInt32(min, 2);
            int secDecimal = Convert.ToInt32(sec, 2);
        }
    }
}