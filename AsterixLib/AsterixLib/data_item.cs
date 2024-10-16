using System;

namespace AsterixLib
{
    // Clase base
    public abstract class DataItem
    {
        // Atributos
        public string info;
        public static string nombreFichero {  get; set; }

        // Constructor
        public DataItem(string bits)
        {
            
            this.info = info;
            
        }

        // Getters y Setters
       
        public string GetInfo()
        {
            return info;
        }

        public void SetInfo(string info)
        {
            this.info = info;
        }

      

        // M�todo abstracto que ser� implementado por las clases hijas
        public abstract void Descodificar();

        // M�todo que escribe en un fichero
        public void EscribirEnFichero(string mensaje)
        {
         
                using (StreamWriter escritor = new StreamWriter(nombreFichero, true))
                {
                    escritor.Write(mensaje);
                }
            

        }
        public static void SetNombreFichero(string nombreFichero)
        {
            nombreFichero = nombreFichero;
        }
    }
}
