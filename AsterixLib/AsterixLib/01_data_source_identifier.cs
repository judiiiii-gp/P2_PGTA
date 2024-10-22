using System;
using System.Diagnostics;
using System.Text;


namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class DataSourceIdentifier : DataItem
    {

       
       
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public DataSourceIdentifier(string info)
            : base(info)
        {
            
        }


        // Implementaci�n del m�todo abstracto Descodificar
        public override void Descodificar()
        {
            int length = 8; //Cada octeto tiene 8 bits

            string SAC = base.info.Substring(0, length);

            string SIC = base.info.Substring(length);

            // Convertir SAC y SIC de binario a decimal
            int sacDecimal = Convert.ToInt32(SAC, 2);
            string SAC_hex = sacDecimal.ToString("X");

            int sicDecimal = Convert.ToInt32(SIC, 2);
            string SIC_hex = sicDecimal.ToString("X");



            // Llamada al m�todo EscribirEnFichero de la clase base
            EscribirEnFichero(SAC_hex + ";"+ SIC_hex + ";", false);

        }
    }
}
