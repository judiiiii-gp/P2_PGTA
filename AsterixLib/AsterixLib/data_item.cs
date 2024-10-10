using System;

namespace DI
{
    // Clase base
    abstract class DataItem
    {
        // Atributos
        private string category;
        private int code;
        private int length;
        private string info;

        // Constructor
        public DataItem(string category, int code, int length, string info)
        {
            this.category = category;
            this.code = code;
            this.info = info;
            this.length = length;
        }

        // Getters y Setters
        public string GetCategory()
        {
            return category;
        }

        public void SetCategory(string category)
        {
            this.category = category;
        }

        public int GetCode()
        {
            return code;
        }

        public void SetCode(int code)
        {
            this.code = code;
        }

        public string GetInfo()
        {
            return info;
        }

        public void SetInfo(string info)
        {
            this.info = info;
        }

        public int GetLength()
        {
            return length;
        }

        public void SetLength(int length)
        {
            this.length = length;
        }

        // M�todo abstracto que ser� implementado por las clases hijas
        public abstract void Descodificar();

        // M�todo que escribe en un fichero
        public void EscribirEnFichero(string mensaje)
        {
            // Aqu� puedes implementar la l�gica para escribir en un archivo
            Console.WriteLine(mensaje); //Tenemos que usar StreamWriter para que los datos se escriban en la misma l�nea
            // Si queremos cambiar de l�nea deberemos poner /n
        }
    }
}
