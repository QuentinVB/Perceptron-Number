using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Core.IO
{
    public class DataImageReader
    {
        const string fileName = "AppSettings.dat";

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

        public static void DisplayValues()
        {
            float aspectRatio;
            string tempDirectory;
            int autoSaveTime;
            bool showStatusBar;

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    aspectRatio = reader.ReadSingle();
                    tempDirectory = reader.ReadString();
                    autoSaveTime = reader.ReadInt32();
                    showStatusBar = reader.ReadBoolean();
                }

                Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                Console.WriteLine("Temp directory is: " + tempDirectory);
                Console.WriteLine("Auto save time set to: " + autoSaveTime);
                Console.WriteLine("Show status bar: " + showStatusBar);
            }
        }

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
    }
}
