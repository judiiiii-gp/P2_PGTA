using Microsoft.VisualBasic.Logging;
using System;
using System.IO;
using System.Net;

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
                    if (firstOctet == 48)
                    {
                        // Leer los siguientes dos octetos(16 bits) como un short
                        ushort variableLength = reader.ReadUInt16();
                        Console.WriteLine($"Variable length en decimal: {variableLength}");

                        // Calcular cuántos bits a leer según la longitud variable
                        int bitsToRead = variableLength - 3 * 8; // Restar 3 octetos (3 * 8 bits)

                        // Asegurarse de que hay suficientes bytes para leer
                        if (bitsToRead > 0)
                        {
                            byte[] buffer = new byte[(bitsToRead + 7) / 8]; // Redondear hacia arriba
                            reader.Read(buffer, 0, buffer.Length);

                            // Convertir el buffer a una representación decimal
                            Console.WriteLine("Bits leídos:");
                            foreach (var b in buffer)
                            {
                                Console.Write($"{b} ");
                            }
                            Console.WriteLine();
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
    }
}
