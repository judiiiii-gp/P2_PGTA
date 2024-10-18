using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class Mode3A : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Mode3A(string info)
            : base(info)
        {

        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al Mode3A");
            string V = base.info.Substring(0, 1);
            if (V == "0")
            {
                V = "Code validated";
            }
            else
            {
                V = "Code not validated";
            }
            string G = base.info.Substring(1,1);
            if (G == "0")
            {
                G = "Default";
            }
            else
            {
                G = "Garbled code";
            }
            string L = base.info.Substring(2,1);
            if (L == "0")
            {
                L = "Mode-3/A code derived from the reply of the transponder";
            }
            else
            {
                L = "Mode-3/A code not extracted during the last scan";
            }
            string SPARE = base.info.Substring(3,1); //Spare bit que siempre será 0
            int message = Convert.ToInt32(base.info.Substring(4), 2);
            string mensaje_octal = Convert.ToString(message, 8);
            //Debug.WriteLine("Tenim el missatge");


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(V + ";" + G + ";" + L + ";" + mensaje_octal + ";");
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}