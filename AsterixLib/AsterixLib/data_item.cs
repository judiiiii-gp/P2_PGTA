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

      

        // Método abstracto que será implementado por las clases hijas
        public abstract void Descodificar();
        public abstract string ObtenerAtributos();

        // Método que escribe en un fichero
        public void EscribirEnFichero(string mensaje, bool Saltolinea=false)
        {
            if (string.IsNullOrEmpty(NombreFichero))
            {
                throw new InvalidOperationException("La ruta del fichero no es válida");
            }

         
            using (StreamWriter escritor = new StreamWriter(NombreFichero, true))
            {
                if (Saltolinea)
                {
                    escritor.WriteLine(mensaje);
                }
                else
                {
                    escritor.Write(mensaje);
                }
               
            }
            

        }
        public static void SetNombreFichero(string nombreFichero)
        {
            NombreFichero = nombreFichero;
        }
    }
}
