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
            /*
            //SourceImage.Render(SourceImage.CreateRandomPicture(20, 20), 20, 20);
            
            //var matrix = SourceImage.GetBWMatrixFromPicture("1579480335.png");
            var matrix = SourceImage.GetBWMatrixFromPicture("source3.png");
            
            Input input = new Input(20, 20, matrix);

            Output<int> output = new Output<int>(new int[10] { 0,1,2,3,4,5,6,7,8,9});
            Network<int> network = new Network<int>(new BasicConfiguration(), input,output);
            
            Console.WriteLine(network.Print());
            Console.ReadLine();

            Console.Clear();
            network.UpdateWeight();
            Console.WriteLine(network.Print());

            
            */

            var dal = new DataAccessLayer();

            //dal.CreateDatabase();
            dal.SaveLayer(new Layer(new BasicConfiguration(), 5),2);

            Console.WriteLine("Finished !");
            Console.ReadLine();
        }
    }
}
