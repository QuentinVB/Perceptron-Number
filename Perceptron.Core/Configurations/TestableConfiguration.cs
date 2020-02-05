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
        readonly bool _saveToDisk;


        public TestableConfiguration()
            :this(2,4, false)
        {
        }
        public TestableConfiguration(int hiddenLayerCount,int neuronPerLayer,bool saveToDisk)
        {
            _hiddenLayerCount = hiddenLayerCount;
            _neuronPerLayer = neuronPerLayer;
            _saveToDisk = saveToDisk;
        }
        public int HiddenLayerCount { get => _hiddenLayerCount; }
        public int NeuronPerLayer { get => _neuronPerLayer; }
        public float BiasSource { get => (float)(_randomSource.NextDouble() * _randomSource.Next(-10, 10)); }
        public float WeightSource { get => (float)(_randomSource.NextDouble() * 0.1f); }
        public Random RandomSource { get => _randomSource; }
        public bool SaveToDisk { get => _saveToDisk; }

        //TODO : make activation function configurable
        public float ActivationFunction(float x) => Neuron.Sigmoid(x);
    }
}
