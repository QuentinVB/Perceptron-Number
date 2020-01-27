using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public struct Link
    {
        public Link(Neuron inputNeuron, Neuron outputNeuron, float weight)
        {
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron; 
            Weight = weight;

            inputNeuron.AddChildren(this);
            outputNeuron.AddParent(this);
        }

        public Neuron InputNeuron { get; }
        public Neuron OutputNeuron { get; }
        public float Weight { get; set; }
        public float PonderateWeight 
        { 
            get {
                if (this.InputNeuron == null) throw new NullReferenceException("The link has no parent neuron !");
                return Weight * InputNeuron.ActivationLevel; 
            } 
        }

        public override string ToString()
        {
            return $"w:{Weight},pw:{PonderateWeight}";
        }

    }
}
