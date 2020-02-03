using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Core.Configurations
{
    
    public class VoidConfiguration : IConfiguration
    {
        Random _randomSource = new Random();
        public int HiddenLayerCount { get => 2; }
        public int NeuronPerLayer { get => 4; }
        public float BiasSource { get =>0; }
        public float WeightSource { get => 0; }
        public Random RandomSource { get => _randomSource; }
        public float ActivationFunction(float x) => Neuron.Sigmoid(x);
    }
}
