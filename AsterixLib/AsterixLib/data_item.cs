using System;

namespace DI
{
    // Clase base
    abstract class DataItem
    {
        // Atributos
        public string category;
        public int code;
        public int length;
        public string info;

        // Constructor
        public DataItem(string category, int code, string info, int length)
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

        // Método abstracto que será implementado por las clases hijas
        public abstract void Descodificar();

        // Método que escribe en un fichero
        public void EscribirEnFichero(string mensaje)
        {
            // Aquí puedes implementar la lógica para escribir en un archivo
            Console.WriteLine(mensaje); //Tenemos que usar StreamWriter para que los datos se escriban en la misma línea
            // Si queremos cambiar de línea deberemos poner /n
        }
    }
}
