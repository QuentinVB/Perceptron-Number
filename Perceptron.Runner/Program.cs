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

            Input input = Input.DefaultInput();
            //Input input = new Input(20, 20, matrix);
            Output<int> output = new Output<int>(new int[10] { 0,1,2,3,4,5,6,7,8,9});
            Network<int> network = new Network<int>(new TestableConfiguration(), input,output);
            
            Console.WriteLine(network.Print());
            Console.ReadLine();

            Console.Clear();
            network.UpdateWeight();
            Console.WriteLine(network.Print());

            var dal = new DataAccessLibrary<int>();

            Console.WriteLine("saving ...");

            dal.CreateDatabase();
            dal.SaveNetwork(network);
            Console.WriteLine("saved");

            network.UpdateNetwork(Input.GetRandomMatrix());

            Console.WriteLine("saving ...");
            dal.UpdateNetwork(network);
            Console.WriteLine("saved");

            //dal.SaveLayer(network.Layers[0],1);

            /*
            Network<int> network2 = new Network<int>(new VoidConfiguration(), input, output);
            Console.WriteLine("restore ...");

            dal.RestoreNetwork(network2);
            Console.WriteLine("restored");
            */

            //dal.SaveParentNeuronLink(network.Layers[2].Neurons[0], 0, 2);

            Console.WriteLine("Finished !");
            Console.ReadLine();
        }
    }
}
