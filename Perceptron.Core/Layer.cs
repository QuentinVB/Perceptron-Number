using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public class Layer
    {
        Neuron[] _neurons;

        //make it enumerable ?
        public Layer(int neuronCount)
            : this(neuronCount, 1, 1) { }

        public Layer(int neuronCount, int topLayerNeuronCount, int bottomLayerNeuronCount)
        {
            if(neuronCount <= 0 ) throw new ArgumentException("Neuron count should be strictly positive. Was " + neuronCount);

            if(topLayerNeuronCount < 0 ) throw new ArgumentException("topLayerNeuron count should be positive. Was " + topLayerNeuronCount);

            if(bottomLayerNeuronCount < 0 ) throw new ArgumentException("bottomLayerNeuron count should be positive. Was " + bottomLayerNeuronCount);

            _neurons = new Neuron[neuronCount];

            FillNeuron(neuronCount, topLayerNeuronCount, bottomLayerNeuronCount);
        }

        private void FillNeuron(int neuronCount, int parentLayerNeuronCount, int childrenLayerNeuronCount)
        {
            for (int i = 0; i < neuronCount; i++)
            {
                _neurons[i]=new Neuron(0.0f, parentLayerNeuronCount, childrenLayerNeuronCount);
            }
        }

        public Neuron[] Neurons { get => _neurons; }

        //best ?
        //order ?

    }
}
