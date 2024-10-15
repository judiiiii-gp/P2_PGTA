using System;

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
            int total = Convert.ToInt32(base.info.Substring(0, 24))*(1/128);
            TimeSpan time = TimeSpan.FromSeconds(total);

            string totalString = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                            time.Hours,
                            time.Minutes,
                            time.Seconds,
                            time.Milliseconds);
            // string str = time .ToString(@"hh\:mm\:ss\:fff"); --> per si peta el string de sobre
        }
    }
}