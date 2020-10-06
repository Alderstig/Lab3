using System;
using System.IO;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string pngSignature = "89504E470D0A1A0A";
            string bmpSignature = "424D";
            string checkIfPng = "";
            string checkIfBmp = "";
            string pngWidth = "";
            string pngHeight = "";
            string bmpWidth = "";
            string bmpHeight = "";
            string path = Path.GetFullPath(args[0]);
            
            if(File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open);
                int fileSize = (int)fs.Length;
                byte[] data = new byte[fileSize];
                fs.Read(data, 0, fileSize);
               
                for (int i = 0; i <= 8; i++)
                {
                    if(i < 8)
                    {
                        checkIfPng += data[i].ToString("X2");
                    }

                    if(i < 2)
                    {
                        checkIfBmp += data[i].ToString("X2");
                    }
                }

                if (checkIfPng == pngSignature)
                {
                    for (int i = 0; i <= 23; i++)
                    {
                        if (i >= 16 && i <= 19)
                        {
                            pngWidth += data[i].ToString("X2");                          
                        }
                        else if (i >= 20 && i <= 23)
                        {
                            pngHeight += data[i].ToString("X2");
                        }
                    }

                    Console.WriteLine($"The file is a .png-file '{path}' Resolution: {HexToDec(pngWidth)} x {HexToDec(pngHeight)}"); 
                }
                else if (checkIfBmp == bmpSignature)
                {
                    for (int i = 25; i >= 18; i--)
                    {
                        if (i >= 18 && i <= 21)
                        {
                            bmpWidth += data[i].ToString("X2");
                        }
                        else if (i >= 22 && i <= 25)
                        {
                            bmpHeight += data[i].ToString("X2");
                        }
                    }
                    Console.WriteLine($"The file is a .bmp-file '{path}' Resolution: {HexToDec(bmpWidth)} x {HexToDec(bmpHeight)}");
                }
                else
                {
                    Console.WriteLine($"The file is neither a .png nor a .bmp-file '{path}'");
                }
            }
            else
            {
                Console.WriteLine($"There was something wrong with your filepath '{path}'");
            }
        }

        public static int HexToDec(string hexVal)
        {
            return Convert.ToInt32(hexVal, 16);
        }
    }
}