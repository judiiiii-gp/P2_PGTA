using System;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class DataSourceIdentifier : DataItem
    {

       
       
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public DataSourceIdentifier(string info)
            : base( info)
        {
            
        }


        // Implementación del método abstracto Descodificar
        public override void Descodificar()
        {
            int length = 8; //Cada octeto tiene 8 bits
            Console.WriteLine("Estem al descodificar de DSI");
            string SAC = base.info.Substring(0, length);
            string SIC = base.info.Substring(length);
            // Convertir SAC y SIC de binario a decimal
            int sacDecimal = Convert.ToInt32(SAC, 2);
            int sicDecimal = Convert.ToInt32(SIC, 2);

            
            // Llamada al método EscribirEnFichero de la clase base
            EscribirEnFichero(Convert.ToString(sacDecimal) + ";"+ Convert.ToString(sicDecimal) + ";");
        }
    }
}
