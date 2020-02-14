using Perceptron.Core;
using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core.IO
{
    //TODO: should be abstract ?
    public class Output<T> : IOutputReader<T> where T : IComparable<T>
    {
        T[] _ouputValues;
        List<OutputAssociation<Neuron, T>> _associations;

        public Output( T[] ouputValues)
        {
            _ouputValues = ouputValues;
            _associations = new List<OutputAssociation<Neuron, T>>();
        }

        public int OutputCount => _ouputValues.Length;

        public T[] OuputValues { get => _ouputValues; set => _ouputValues = value; }

        public List<OutputAssociation<Neuron, T>> Associations {
            get
            {
                return _associations;
            }
        }

        

        public T ReadResult()
        {
            if (_associations.Count == 0) throw new InvalidOperationException("The association should be made before reading");

            OutputAssociation<Neuron, T> maxValue=new OutputAssociation<Neuron, T>(default, Neuron.MinDefault());
            foreach (OutputAssociation<Neuron, T> item in _associations)
            {
                if(item.LinkedNeuron.ActivationLevel>maxValue.LinkedNeuron.ActivationLevel)
                {
                    maxValue = item;
                }
            }
            return maxValue.ValueOut;
        }

        public static Output<T> DefaultOutput(T defaultValue)
        {
            int outputCount = 10;
            T[] outputValues = new T[outputCount];
            for (int i = 0; i < outputCount; i++)
            {
                outputValues[i] = defaultValue;
            }

            return new Output<T>(outputValues);
        }

        /// <summary>
        /// sum the squared difference between actual output minus the desired output
        /// </summary>
        /// <param name="label">the desired matching label</param>
        /// <returns></returns>
        public float ComputeCost(T label)
        {            
            float cost = 0;


            for (int i = 0; i < OutputCount; i++)
            {
                if(label.CompareTo(_associations[i].ValueOut) == 0 )
                {
                     cost += (float)Math.Pow(_associations[i].LinkedNeuron.ActivationLevel - 1.0f,2);
                }
                else
                {
                    cost += (float)Math.Pow(_associations[i].LinkedNeuron.ActivationLevel, 2);
                }
            }
            return cost;
        }
    }
}
