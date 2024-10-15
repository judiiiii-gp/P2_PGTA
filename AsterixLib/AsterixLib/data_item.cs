using System;

namespace AsterixLib
{
    // Clase base
    public abstract class DataItem
    {
        // Atributos
        public string info;

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
            try
            {
                using (StreamWriter escritor)
            }
        }
    }
}
