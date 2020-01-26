using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron_Number
{
    public struct Link
    {
        readonly Neuron _inputNeuron;
        readonly Neuron _outputNeuron;
        float _weight;

        public Link(Neuron inputNeuron, Neuron outputNeuron, float weight)
        {
            _inputNeuron = inputNeuron;


            _outputNeuron = outputNeuron;
   
            _weight = weight;
        }

        public Neuron InputNeuron => _inputNeuron;
        public Neuron OutputNeuron => _outputNeuron;
        public float Weight { get => _weight; set => _weight = value; }
        public float PonderateWeight { get => _weight*_inputNeuron.ActivationLevel;  }

    }
}
