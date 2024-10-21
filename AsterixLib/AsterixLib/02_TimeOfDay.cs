using System;
using System.Diagnostics;

namespace AsterixLib
{
    // Clase hija que hereda de DataItem
    public class TimeOfDay : DataItem
    {
        // Constructor que inicializa las variables utilizando el constructor de la clase base
        public TimeOfDay( string info)
            : base(info)
        {

        }
        public override void Descodificar()
        {
            //Debug.WriteLine("Estem al TimeOfDay");
            long total =Convert.ToInt64(base.info.Substring(0, 24), 2);
            //Debug.WriteLine("Hem tallat la string: "+ total);
            total = total / 128;
            //Debug.WriteLine("Tenim el int");
            TimeSpan time = TimeSpan.FromSeconds(total);
            //Debug.WriteLine("Hem agafat el TimeSpan");
            string totalString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                            time.Hours,
                            time.Minutes,
                            time.Seconds,
                            time.Milliseconds);
            //Debug.WriteLine("Info del time: " + totalString);
            // string str = time .ToString(@"hh\:mm\:ss\:fff"); --> per si peta el string de sobre
            EscribirEnFichero(totalString + ";");
            //Debug.WriteLine("Hem escrit al fitxer");
        }
    }
}