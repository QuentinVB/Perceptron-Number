using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Perceptron.ImageInput
{
    public class SourceImage
    {
        public static float[,] GetBWMatrixFromPicture(string pictureName)
        {
            string path = Directory.GetCurrentDirectory()  + @"\input\" + pictureName;
            float[,] matrix;
            //read
            using (Bitmap bmp = new Bitmap(path))
            {
                matrix = new float[bmp.Width, bmp.Height];
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        matrix[x,y]=pixelColor.GetBrightness();   
                    }
                }

            }
            if (matrix == null) throw new ArgumentNullException("Matrix is null");
            return matrix;
        }
        public static float[,] CreateRandomPicture(int width, int height)
        {
            Random rand = new Random();
            float[,] matrix = new float[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = (float)rand.NextDouble();
                }
            }
            return matrix;
        }
        public static void Render(float[,] matrix, int width, int height)
        {
            try
            {
                //draw
                Bitmap bmp = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        float matrixvalue = Math.Abs(matrix[x, y]);
                        
                        int value = (matrixvalue > 1) ? 255 : (int)(matrixvalue * 255);
                        Color newColor = Color.FromArgb(value, value, value);
                        bmp.SetPixel(x, y, newColor);
                    }
                }
                //save
                Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\output\\"));
                bmp.Save($".\\output\\{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png", ImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception("An error occured while generating the bitmap "+e);
            }
        }

        public static void Render(byte[] firstImage, uint width, uint height)
        {
            try
            {
                //draw
                Bitmap bmp = new Bitmap((int)width, (int)height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int idx = x + y * (int)width;
                        Color newColor = Color.FromArgb(Math.Abs(firstImage[idx]), Math.Abs(firstImage[idx]), Math.Abs(firstImage[idx]));
                        bmp.SetPixel(x, y, newColor);
                    }
                }
                //save
                Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\output\\"));
                bmp.Save($".\\output\\{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png", ImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception("An error occured while generating the bitmap " + e);
            }
        }
    }
}
