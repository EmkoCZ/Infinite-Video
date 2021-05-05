using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InfiniteVideo
{
    public static class ByteHelper
    {
        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Video mvhd editor";
            menu();
        }

        private static void menu()
        {
            Console.WriteLine("Enter file path: ");
            string file = Console.ReadLine();

            List<string> lines = new List<string>();

            if (File.Exists(file))
            {
                int mvhd = 0;

                byte[] bytes = File.ReadAllBytes(file);
                for (int i = 0; i < bytes.Length; i++)
                {
                    byte b = bytes[i];

                    lines.Add($"{i}. " + b.ToString("X"));


                    if (b.ToString("X") == "6D" && bytes[i + 1].ToString("X") == "76" && bytes[i + 2].ToString("X") == "68" && bytes[i + 3].ToString("X") == "64")
                        mvhd = i;

                }

                int leng = mvhd + 18;
                bytes[leng] = 0;
                leng++;
                bytes[leng] = 1;
                leng++;
                bytes[leng] = 127;
                leng++;
                bytes[leng] = 255;
                leng++;
                bytes[leng] = 255;
                leng++;
                bytes[leng] = 255;


                File.WriteAllBytes(file, bytes);
                Console.WriteLine("Done");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("File does not exist!");
                menu();
            }

            Console.ReadLine();
        }
    }

    
}
