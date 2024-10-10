﻿using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class FlightLevel : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public FlightLevel(string category, int code, int length, string info)
            : base(category, code, info, length)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {

            string V = base.info.Substring(0, 1);
            if (V == '0')
            {
                V = "Code validated";
            }
            else
            {
                V = "Code not validated";
            }
            string G = base.info.Substring(1, 1);
            if (G == '0')
            {
                G = "Default";
            }
            else
            {
                G = "Garbled code";
            }
                       
            int message = Convert.ToInt32(base.info.Substring(2), 2);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(V + ";" + G + ";" + Convert.ToString(message) + ";");
        }
    }
}
