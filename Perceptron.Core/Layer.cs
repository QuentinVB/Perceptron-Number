using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public class Layer
    {
        Neuron[] _neurons;
        Random _randomsource;

        //make it enumerable ?
        public Layer(int neuronCount)
            : this(neuronCount, 1, 1, new Random()) { }
        public Layer(int neuronCount, int topLayerNeuronCount, int bottomLayerNeuronCount)
            : this(neuronCount, topLayerNeuronCount, bottomLayerNeuronCount, new Random()) { }

        public Layer(int neuronCount, int topLayerNeuronCount, int bottomLayerNeuronCount, Random randomsource)
        {
            if(neuronCount <= 0 ) throw new ArgumentException("Neuron count should be strictly positive. Was " + neuronCount);

            if(topLayerNeuronCount < 0 ) throw new ArgumentException("topLayerNeuron count should be positive. Was " + topLayerNeuronCount);

            if(bottomLayerNeuronCount < 0 ) throw new ArgumentException("bottomLayerNeuron count should be positive. Was " + bottomLayerNeuronCount);

            _randomsource = randomsource;
            _neurons = new Neuron[neuronCount];

            FillNeuron(neuronCount, topLayerNeuronCount, bottomLayerNeuronCount);
        }

        private void FillNeuron(int neuronCount, int parentLayerNeuronCount, int childrenLayerNeuronCount)
        {
            for (int i = 0; i < neuronCount; i++)
            {
                _neurons[i]=new Neuron(
                    0.0f, 
                    parentLayerNeuronCount, 
                    childrenLayerNeuronCount, 
                    (float)(_randomsource.NextDouble() * _randomsource.Next(-10,10))
                );
            }
        }

        public Neuron[] Neurons { get => _neurons; }

        //best ?
        //order ?

    }
}
