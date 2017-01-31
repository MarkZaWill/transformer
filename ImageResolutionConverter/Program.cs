using System;
using System.Drawing;
using System.IO;



namespace ImageResolutionConverter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            string fileNameOrigin = Console.ReadLine();
    
            GetPhoto(fileNameOrigin);
            Console.WriteLine(fileNameOrigin);
        }

        public static byte[] GetPhoto(string FileNameOrigin)
        {

            
            FileStream stream = new FileStream(
                FileNameOrigin, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);
            using (Bitmap bitmap = (Bitmap)Image.FromStream(stream))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {

                    string[] lowRes = FileNameOrigin.Split('.');
                    string changeFile = lowRes[lowRes.Length - 2];
                    string NewchangeFile = changeFile + "lowres";
                  
                    Console.WriteLine(NewchangeFile);
                    Console.ReadLine();
                    string lowResFile = String.Join(" ", lowRes);
                    string lowResArray = lowResFile.Replace(changeFile, NewchangeFile);
                    lowResArray = lowResArray.Replace(" ", ".");
                    Console.WriteLine(lowResArray);
                    Console.ReadLine();

                    newBitmap.SetResolution(72, 72);
                    int Height = newBitmap.Height;
                    int Width = newBitmap.Width;

                    Bitmap resized = new Bitmap(newBitmap, new Size(newBitmap.Width / 4, newBitmap.Height / 4));

                    resized.SetResolution(72, 72);

                    Console.WriteLine(newBitmap.Height);
                    Console.WriteLine(newBitmap.Width);

                    Console.WriteLine(lowResArray);

                    resized.Save(lowResArray);
                }

                reader.Close();
                stream.Close();
                return photo;
            }


        }
    }
}


