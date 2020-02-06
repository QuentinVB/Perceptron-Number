using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.ImageInput
{
    public class DataImageReader
    {
        /*
        public static void WriteDefaultValues()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(1.250F);
                writer.Write(@"c:\Temp");
                writer.Write(10);
                writer.Write(true);
            }
        }
        */

        public static void ReadLabelFile(string filename)
        {
            uint magicNumber;
            uint numberOfLabels;
            byte[] labels;

            if (File.Exists(filename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {
                    magicNumber = reader.ReadUInt32BE();
                    numberOfLabels = reader.ReadUInt32BE();
                    labels = new byte[numberOfLabels];
                    for (int i = 0; i < numberOfLabels; i++)
                    {
                        labels[i] = reader.ReadByte();
                    }
                }

                Console.WriteLine("magicNumber set to: " + magicNumber);
                Console.WriteLine("numberOfLabels is: " + numberOfLabels);
            }
        }

        public static void ReadImageFile(string filename)
        {
            uint magicNumber;
            uint numberOfImages;
            uint numberOfRows;
            uint numberOfColumns;
            byte[] pixels;

            if (File.Exists(filename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {
                    magicNumber = reader.ReadUInt32BE();
                    numberOfImages = reader.ReadUInt32BE();
                    numberOfRows = reader.ReadUInt32BE();
                    numberOfColumns = reader.ReadUInt32BE();
                    pixels = new byte[numberOfImages*numberOfRows * numberOfColumns];
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i] = reader.ReadByte();
                    }
                }

                Console.WriteLine("magicNumber set to: " + magicNumber);
                Console.WriteLine("numberOfImages is: " + numberOfImages);
                Console.WriteLine("numberOfRows is: " + numberOfRows);
                Console.WriteLine("numberOfColumns is: " + numberOfColumns);
            }
        }

        public static ImageDataBlock[] LoadImageData(string labelFilename,string imageFilename)
        {
            uint numberOfImages=0;
            uint numberOfRows;
            uint numberOfColumns;
            byte[] pixels;

            if (File.Exists(imageFilename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(imageFilename, FileMode.Open)))
                {
                    reader.ReadUInt32BE();
                    numberOfImages = reader.ReadUInt32BE();
                    numberOfRows = reader.ReadUInt32BE();
                    numberOfColumns = reader.ReadUInt32BE();
                    pixels = new byte[numberOfImages * numberOfRows * numberOfColumns];
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i] = reader.ReadByte();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }


            uint numberOfLabels =0;
            byte[] labels;

            if (File.Exists(labelFilename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(labelFilename, FileMode.Open)))
                {
                    reader.ReadUInt32BE();
                    numberOfLabels = reader.ReadUInt32BE();
                    labels = new byte[numberOfLabels];
                    for (int i = 0; i < numberOfLabels; i++)
                    {
                        labels[i] = reader.ReadByte();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            if (numberOfImages != numberOfLabels) throw new InvalidDataException("label and image should have the same count");

            ImageDataBlock[] imagesDataBlocks= new ImageDataBlock[numberOfLabels];
            DataBlockCommonInfo commonInfo = new DataBlockCommonInfo() { Height = (int)numberOfColumns, Width = (int)numberOfRows };

            for (int i = 0; i < numberOfLabels; i++)
            {

                byte[] image = new byte[numberOfRows * numberOfColumns];

                Array.Copy(pixels, i*(numberOfRows* numberOfColumns), image, 0, numberOfRows * numberOfColumns);
                imagesDataBlocks[i] = new ImageDataBlock(image, commonInfo, labels[i]);
            }

            return imagesDataBlocks;
        }



        public static void PrintImageFromFile(string filename)
        {
            uint magicNumber;
            uint numberOfImages;
            uint numberOfRows=0;
            uint numberOfColumns=0;
            byte[] pixels=null;

            if (File.Exists(filename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {
                    magicNumber = reader.ReadUInt32BE();
                    numberOfImages = reader.ReadUInt32BE();
                    numberOfRows = reader.ReadUInt32BE();
                    numberOfColumns = reader.ReadUInt32BE();

                    pixels = new byte[numberOfImages * numberOfRows * numberOfColumns];
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i] = reader.ReadByte();
                    }
                }
            }

            //get label

            //first image
            byte[] firstImage= new byte[numberOfRows * numberOfColumns];

            Array.Copy(pixels, 1, firstImage, 0,  numberOfRows * numberOfColumns);

            SourceImage.Render(firstImage, numberOfRows, numberOfColumns);
        }
    }
}
