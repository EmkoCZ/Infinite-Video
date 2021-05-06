using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InfiniteVideo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Video mvhd editor";
            menu();
        }

        private static void menu()
        {
            Console.WriteLine("1. Create infinite video");
            Console.WriteLine("2. Create discord crashing video");
            int input = Int32.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    createInfiniteVideo();
                    break;
                case 2:
                    createCrashVideo();
                    break;
                default:
                    menu();
                    break;
            }

            Console.WriteLine("Enter file path: ");
            string file = Console.ReadLine();

            Console.WriteLine("Enter file path: ");
            string file2 = Console.ReadLine();

            

            string[] files = { file, file2 };
            FFMPEG.ConcateVideo(files, "D:Testing/crash/code.mp4");
            Console.WriteLine("DOne");

            Console.ReadLine();
        }

        static void createInfiniteVideo()
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
                Console.WriteLine("Finished creating infinite video, press enter to continue");
                Console.ReadLine();
                Console.Clear();
                menu();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File with name {file} does not exist!\n");
                Console.ResetColor();
                menu();
            }
        }

        static void createCrashVideo()
        {
            Console.WriteLine("How many videos do you want to join together?");
            int count = Int32.Parse(Console.ReadLine());

            List<string> files = new List<string>();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i}. File path: ");
                files.Add(Console.ReadLine());
            }

            Console.WriteLine("Enter output file name/path: ");
            string outp = Console.ReadLine();

            FFMPEG.ConcateVideo(files.ToArray(), outp);
            Console.WriteLine("Finished creating crash video, press enter to continue");
            Console.ReadLine();
            Console.Clear();
            menu();
        }
    }
}

