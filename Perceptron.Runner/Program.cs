using Perceptron.Core;
using Perceptron.Core.Configurations;
using Perceptron.Core.IO;
using Perceptron.ImageInput;
using System;

namespace Perceptron.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //SourceImage.Render(SourceImage.CreateRandomPicture(20, 20), 20, 20);
            
            //var matrix = SourceImage.GetBWMatrixFromPicture("1579480335.png");
            //var matrix = SourceImage.GetBWMatrixFromPicture("source3.png");
            Console.WriteLine("Creating network...");

            //Input input = Input.DefaultInput();
            Input input = new Input(28, 28);
            Output<int> output = new Output<int>(new int[10] { 0,1,2,3,4,5,6,7,8,9});
            Network<int> network = new Network<int>(new TestableConfiguration(2,4,false), input,output);
            Console.WriteLine("Loading tests data...");
            var imageDataBlocks = DataImageReader.LoadImageData("train-labels.idx1-ubyte", "train-images.idx3-ubyte");
            Console.WriteLine("Done !");



            Console.WriteLine(network.Print());
            Console.ReadLine();

            

            Console.Clear();
            network.TestingNetworkAccuracy(imageDataBlocks, 200 );
            Console.WriteLine(network.Print());
            Console.WriteLine(network.ToString());


    


            

            Console.WriteLine("Finished !");
            Console.ReadLine();
        }
    }
}
