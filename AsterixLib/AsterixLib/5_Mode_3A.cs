using System;

namespace DI
{
    // Clase hija que hereda de DataItem
    class Mode3A : DataItem
    {




        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public Mode3A(string category, int code, int length, string info)
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
            string G = base.info.Substring(1,1);
            if (G == '0')
            {
                G = "Default";
            }
            else
            {
                G = "Garbled code";
            }
            string L = base.info.Substring(2,1);
            if (L == '0')
            {
                L = "Mode-3/A code derived from the reply of the transponder";
            }
            else
            {
                L = "Mode-3/A code not extracted during the last scan";
            }
            string SPARE = base.info.Substring(3,1); //Spare bit que siempre será 0
            int message = Convert.ToInt32(base.info.Substring(4), 8);


            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(V + ";" + G + ";" + L + ";" + Convert.ToString(message) + ";");
        }
    }
}