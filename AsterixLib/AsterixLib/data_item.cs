using System;

namespace AsterixLib
{
    // Clase base
    public abstract class DataItem
    {
        // Atributos
        public string info;
        public static string NombreFichero {  get; set; }

        // Constructor
        public DataItem(string info)
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
            if (string.IsNullOrEmpty(NombreFichero))
            {
                throw new InvalidOperationException("La ruta del fichero no es v�lida");
            }
         
            using (StreamWriter escritor = new StreamWriter(NombreFichero, true))
            {
                escritor.Write(mensaje);
            }
            

        }
        public static void SetNombreFichero(string nombreFichero)
        {
            NombreFichero = nombreFichero;
        }
    }
}
