using System;
using System.IO;

namespace InfiniteVideo
{
    public static class FFMPEG
    {
        public static void ConcateVideo(string[] files, string outPutFile)
        {
            foreach (var item in files)
            {
                if (!File.Exists(item))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"File with name {item} does not exist!");
                    Console.ResetColor();
                    return;
                }
            }

            var outp = Directory.GetParent(outPutFile);
            string listFile = outp.ToString() + "\\list.txt";
            var f = File.Create(listFile);
            f.Close();

            string[] finalList = new string[files.Length];
            

            foreach (var item in files)
            {
                int leng = item.Split('\\').Length;
                string file = item.Split('\\')[leng - 1];
                File.AppendAllText(listFile, "file " + file + Environment.NewLine);
            }

            string args = $"-f concat -i {listFile} -codec copy {outPutFile}";

            System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "\\ffmpeg.exe", args);
        }
    }
}

