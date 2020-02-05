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
            
            var matrix = SourceImage.GetBWMatrixFromPicture("1579480335.png");
            //var matrix = SourceImage.GetBWMatrixFromPicture("source3.png");
            Console.WriteLine("Creating network...");

            Input input = Input.DefaultInput();
            //Input input = new Input(20, 20, matrix);
            Output<int> output = new Output<int>(new int[10] { 0,1,2,3,4,5,6,7,8,9});
            Network<int> network = new Network<int>(new TestableConfiguration(2,4,true), input,output);
            Console.WriteLine("Done !");



            Console.WriteLine(network.Print());
            Console.ReadLine();

            

            Console.Clear();
            network.UpdateNetwork(Input.GetRandomMatrix());
            Console.WriteLine(network.Print());



            Console.WriteLine("Finished !");
            Console.ReadLine();
        }
    }
}
