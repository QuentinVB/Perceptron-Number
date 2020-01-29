using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Core.Configurations
{
    
    public class TestableConfiguration : IConfiguration
    {
        Random _randomSource = new Random();
        readonly int _hiddenLayerCount;
        readonly int _neuronPerLayer;


        public TestableConfiguration()
            :this(2,8)
        {
        }
        public TestableConfiguration(int hiddenLayerCount,int neuronPerLayer)
        {
            _hiddenLayerCount = hiddenLayerCount;
            _neuronPerLayer = neuronPerLayer;
        }
        public int HiddenLayerCount { get => _hiddenLayerCount; }
        public int NeuronPerLayer { get => _neuronPerLayer; }
        public float BiasSource { get => (float)(_randomSource.NextDouble() * _randomSource.Next(-10, 10)); }
        public float WeightSource { get => (float)(_randomSource.NextDouble() * 0.1f); }
        public Random RandomSource { get => _randomSource; }
        //TODO : make activation function configurable
        public float ActivationFunction(float x) => Neuron.Sigmoid(x);
    }
}
