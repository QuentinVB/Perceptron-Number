using Perceptron_Number.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron_Number
{
    public class Network<T>
    {
        readonly IInputLayer _inputLayer;
        readonly IOutputReader<T> _outputLayer;
        readonly Layer[] _layers;
        readonly int _neuronPerLayer;
        readonly int _hiddenLayerCount;
        public Network(int hiddenLayerCount,int neuronPerLayer, IInputLayer inputLayer, IOutputReader<T> outputLayer)
        {
            if (hiddenLayerCount <= 0) throw new ArgumentException("Layer count should be positive");
            if (neuronPerLayer <= 0) throw new ArgumentException("Neuron per layer should be positive");
            _neuronPerLayer = neuronPerLayer;
            _hiddenLayerCount = hiddenLayerCount;

            _inputLayer = inputLayer;
            _outputLayer = outputLayer;

            _layers = new Layer[hiddenLayerCount+2];

            AttachInput(inputLayer);
            CreateHiddenLayer(hiddenLayerCount);
            AttachOutput(outputLayer, hiddenLayerCount);

            //TODO : debug this
            BindAllNeuron();
        }

        private void BindAllNeuron()
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                Neuron[] neurons = _layers[i].Neurons;
                for (int j = 0; j < neurons.Length; j++)
                {
                    if(i + 1 < _layers.Length)
                    {
                        Neuron[] nextNeurons = _layers[i + 1].Neurons;
                        for (int k = 0; k < nextNeurons.Length; k++)
                        {
                            Link link = new Link(neurons[j], neurons[k], 0.0f);
                            neurons[j].AddChildren(link);
                            neurons[k].AddParent(link);
                        }
                    }                       
                }

            }
        }

        public Layer[] Layers { get => _layers; }
        //TODO : make it lazy
        public int NeuronCount { get {
                int count = 0;
                if (Layers.Length == 0) return 0;
                foreach (Layer layer in Layers)
                {
                    count += layer.Neurons.Length;
                }
                return count;         
            } }

        public IInputLayer InputLayer => _inputLayer;
        public IOutputReader<T> OutputLayer => _outputLayer;

        private void CreateHiddenLayer(int hiddenLayerCount)
        {
            if (_inputLayer == null || _outputLayer == null) throw  new InvalidOperationException("Layer creation order is invalid");
            int topCount = _inputLayer.Width * _inputLayer.Height;
            int bottomCount = _outputLayer.OutputCount;
            for (int i = 1; i < hiddenLayerCount+2; i++)
            {
                _layers[i] = new Layer(
                    _neuronPerLayer, 
                    (i == 1) ? topCount : _neuronPerLayer, 
                    (i == hiddenLayerCount+2) ? bottomCount : _neuronPerLayer
                    ) ;
            }
        }


        private void AttachInput(IInputLayer inputLayer)
        {
            int neuronPerLayer = inputLayer.Width * inputLayer.Height;

            _layers[0] = new Layer(neuronPerLayer,0, _neuronPerLayer);

            inputLayer.LoadLayer(_layers[0]);
        }

        private void AttachOutput(IOutputReader<T> outputLayer,int layerPosition)
        {
            _layers[layerPosition] = new Layer(outputLayer.OutputCount, _neuronPerLayer,0);

            for (int i = 0; i < _layers[layerPosition].Neurons.Length-1 ; i++)
            {
                outputLayer.Associations.Add(
                    new OutputAssociation<Neuron, T>(
                        outputLayer.OuputValues[i],
                        _layers[layerPosition].Neurons[i]
                        )
                    );
            }          
        }

        public T ReadResult()
        {
            //TODO: Read based on input, then compute ?
            return _outputLayer.ReadResult();
        }

        public int TotalVariable { get
            {
                return NeuronCount * 2 
                    + (_inputLayer.Height * _inputLayer.Width) * _neuronPerLayer
                    +_hiddenLayerCount* _neuronPerLayer               
                 ;
            } 
        }

        public void UpdateWeight()
        {
            //TODO : Add security here
            for (int i = 1; i < Layers.Length; i++)
            {
                Layer layer = Layers[i];
                for (int j = 0; j < Layers[i].Neurons.Length; j++)
                {
                    layer.Neurons[j].ActivationLevel = layer.Neurons[j].ComputeWeight();
                }
            }
        }
    }
}
