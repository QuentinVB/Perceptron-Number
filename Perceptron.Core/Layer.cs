using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public class Layer
    {
        Neuron[] _neurons;

        //make it enumerable ?
        public Layer(int neuronCount, IConfiguration configuration)
            : this(neuronCount, 1, 1, configuration) { }
        
        public Layer(int neuronCount, int topLayerNeuronCount, int bottomLayerNeuronCount, IConfiguration configuration)
        {
            if (neuronCount <= 0) throw new ArgumentException("Neuron count should be strictly positive. Was " + neuronCount);
            if (topLayerNeuronCount < 0) throw new ArgumentException("TopLayerNeuron count should be positive. Was " + topLayerNeuronCount);
            if (bottomLayerNeuronCount < 0) throw new ArgumentException("BottomLayerNeuron count should be positive. Was " + bottomLayerNeuronCount);

            Configuration = configuration;
            _neurons = new Neuron[neuronCount];

            FillNeuron(neuronCount, topLayerNeuronCount, bottomLayerNeuronCount);
        }

        private void FillNeuron(int neuronCount, int parentLayerNeuronCount, int childrenLayerNeuronCount)
        {
            for (int i = 0; i < neuronCount; i++)
            {
                _neurons[i] = new Neuron(
                    0.0f,
                    parentLayerNeuronCount,
                    childrenLayerNeuronCount,
                    Configuration.BiasSource
                );
            }
        }
        public IConfiguration Configuration { get;}

        public Neuron[] Neurons { get => _neurons; }

        //best ?
        //order ?

    }
}
