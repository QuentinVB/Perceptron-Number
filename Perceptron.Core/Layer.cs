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
        public Layer(IConfiguration configuration, int neuronQuantity)
            : this(configuration, neuronQuantity, 0,0) { }

        /// <summary>
        /// Create a new layer for the neural network
        /// </summary>
        /// <param name="configuration">The global configuration object</param>
        /// <param name="neuronQuantity">The neuron quantity for this layer </param>
        /// <param name="topLayerNeuronCount">The neuron quantity for the layer above</param>
        /// <param name="bottomLayerNeuronCount">The neuron quantity for the layer below</param>
        public Layer(
            IConfiguration configuration,
            int neuronQuantity, 
            int topLayerNeuronCount, 
            int bottomLayerNeuronCount)
        {
            if (neuronQuantity <= 0) throw new ArgumentException("Neuron count should be strictly positive. Was " + neuronQuantity);
            if (topLayerNeuronCount < 0) throw new ArgumentException("TopLayerNeuron count should be positive. Was " + topLayerNeuronCount);
            if (bottomLayerNeuronCount < 0) throw new ArgumentException("BottomLayerNeuron count should be positive. Was " + bottomLayerNeuronCount);

            Configuration = configuration;
            _neurons = new Neuron[neuronQuantity];

            FillNeuron(neuronQuantity, topLayerNeuronCount, bottomLayerNeuronCount);
        }

        private void FillNeuron(int neuronCount, int parentLayerNeuronCount, int childrenLayerNeuronCount)
        {
            for (int i = 0; i < neuronCount; i++)
            {
                _neurons[i] = new Neuron(
                    0.0f,
                    parentLayerNeuronCount,
                    childrenLayerNeuronCount,
                    Configuration.BiasSource,
                    Configuration.ActivationFunction
                );
            }
        }
        public IConfiguration Configuration { get;}

        public Neuron[] Neurons { get => _neurons; }

        //best ?
        //order ?

    }
}
