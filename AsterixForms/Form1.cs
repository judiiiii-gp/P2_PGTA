using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AsterixForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static void ReadBinaryFile(string filePath)
        {   
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    // Leer el primer octeto (8 bits)
                    byte firstOctet = reader.ReadByte();
                    Console.WriteLine($"Primer octeto en decimal: {firstOctet}");
                    if (firstOctet == 48) //Només agafem els data blocks de la cat 48
                    {
                        // Leer los siguientes dos octetos(16 bits) como un short
                        ushort variableLength = reader.ReadUInt16();
                        Console.WriteLine($"Variable length en decimal: {variableLength}");

                        // Calcular cuántos bits a leer según la longitud variable
                        int bitsToRead = variableLength - 3 * 8; // Restamos los octetos de cat y length

                        // Asegurarse de que hay suficientes bytes para leer
                        if (bitsToRead > 0)
                        {
                            byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                            reader.Read(buffer, 0, buffer.Length);
                            string DataBlock = ConvertirByte2String(buffer); //Dades que tenim en un DataBlock

                            //Ara hem de mirar el FSPEC per saber quants DataItems tenim al record 
                            int FSPEC_bits = FSPEC(DataBlock); //Obtenim quants bits té el FSPEC

                        }
                        else
                        {
                            Console.WriteLine("No hay bits suficientes para leer después de restar 3 octetos.");
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
        static string ConvertirByte2String(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0')); //El PadLeft nos asegura que cada byte se representa con 8 bits, rellenando si es necesario
                //Com que sempre hi han octets jo crec que està bé que faci això
            }
            return sb.ToString();
        }



        //Llegim el FSPEC per saber on comencen els Data Items
        static int FSPEC(string DataBlock)
        {
            int length = DataBlock.Length; //No hem de superar mai la longitud

            for (int i=0; i<length; ++i)
            {
                if (i + 8 <= length)
                {
                    string FSPEC = DataBlock.Substring(i, 8);

                    char ultimbit = FSPEC[i+8]; //Busquem el valor de l'últim bit per saber si hi ha més FSPEC

                    if(ultimbit == '0')
                    {
                        return i+8; //Obtenim quants bits de FSPEC hi ha 
                    }

                }
            }
            return -1; //Si hi ha algun error 

        }
    }
}
