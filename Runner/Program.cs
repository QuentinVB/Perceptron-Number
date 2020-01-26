using ImageInput;
using Perceptron_Number;
using System;
using Perceptron_Number.IO;
namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            //SourceImage.Render(SourceImage.CreateRandomPicture(20, 20), 20, 20);
            
            var matrix = SourceImage.GetBWMatrixFromPicture("1579480335.png");

            Input input = new Input(20, 20, matrix);

            Output<int> output = Output<int>.DefaultOutput(default(int));
            Network<int> network = new Network<int>(2, 4, input,output);

            network.UpdateWeight();
            Console.WriteLine("Hello World!");
        }
    }
}
