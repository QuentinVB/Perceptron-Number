using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Core.Configurations
{
    
    public class BasicConfiguration : IConfiguration
    {
        Random _randomSource = new Random();
        public int HiddenLayerCount { get => 2; }
        public int NeuronPerLayer { get => 8; }
        public float BiasSource { get => (float)(_randomSource.NextDouble() * _randomSource.Next(-10, 10)); }
        public float WeightSource { get => (float)(_randomSource.NextDouble() * 0.1f); }
        public Random RandomSource { get => _randomSource; }
        public float ActivationFunction(float x) => Neuron.Sigmoid(x);
    }
}
