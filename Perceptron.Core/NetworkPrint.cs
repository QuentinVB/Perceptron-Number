using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public partial class Network<T>
    {
        public override string ToString()
        {
            return $"Layers : {this.Layers.Length}, TotalNeuronCount : {this.NeuronCount}";
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Layers.Length; i++)
            {
                sb.Append($"l:{i} ");

                for (int j = 0; j < Layers[i].Neurons.Length; j++)
                {
                    sb.Append(ToAscii(Layers[i].Neurons[j].ActivationLevel));
                    sb.Append(" ");
                }
                sb.Append("\n");
                if(i == Layers.Length-1)
                {
                    sb.Append("    ");
                    for (int j = 0; j < Layers[i].Neurons.Length; j++)
                    {
                        sb.Append(OutputLayer.OuputValues[j] + " ");
                    }
                }
            }
            return sb.ToString();
        }

        private char ToAscii(float activationLevel)
        {

            if (activationLevel < 0.2f) return ' ';
            if (activationLevel < 0.4f)  return '░';
            if (activationLevel < 0.6f)   return '▒';
            if (activationLevel < 0.6f)    return '▓';
            if (activationLevel < 0.8f)     return '█';
            if (activationLevel <= 1f) return '█';
            return ' ';
        }
    }
}
