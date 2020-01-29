using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public partial class Network<T>
    {
        Random randomSource;
        readonly IInputLayer _inputLayer;
        readonly IOutputReader<T> _outputLayer;
        readonly Layer[] _layers;

        readonly IConfiguration _configuration;

        public Network(
            IConfiguration configuration, 
            IInputLayer inputLayer, 
            IOutputReader<T> outputLayer
            )
        {
            if (configuration.HiddenLayerCount <= 0) throw new ArgumentException("Layer count should be positive");
            if (configuration.NeuronPerLayer <= 0) throw new ArgumentException("Neuron per layer should be positive");

            _configuration = configuration;

            randomSource = new Random();

            _inputLayer = inputLayer;
            _outputLayer = outputLayer;

            _layers = new Layer[configuration.HiddenLayerCount+ 2];

            AttachInput(inputLayer);
            CreateHiddenLayer();
            AttachOutput(outputLayer, configuration.HiddenLayerCount + 1);

            BindAllNeuron();
        }

        private void BindAllNeuron()
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                Neuron[] neurons = _layers[i].Neurons;
                for (int j = 0; j < neurons.Length; j++)
                {
                    if(i  < _layers.Length-1)
                    {
                        Neuron[] nextNeurons = _layers[i + 1].Neurons;
                        for (int k = 0; k < nextNeurons.Length; k++)
                        {
                            _ = new Link(
                                neurons[j], 
                                nextNeurons[k], 
                                Configuration.BiasSource);
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
        public IConfiguration Configuration => _configuration;
        public IInputLayer InputLayer => _inputLayer;
        public IOutputReader<T> OutputLayer => _outputLayer;

        private void CreateHiddenLayer()
        {
            if (_inputLayer == null || _outputLayer == null) throw  new InvalidOperationException("Layer creation order is invalid");
            int topCount = _inputLayer.Width * _inputLayer.Height;
            int bottomCount = _outputLayer.OutputCount;
            for (int i = 1; i < Configuration .HiddenLayerCount+ 1; i++)
            {
                _layers[i] = new Layer(
                    Configuration,
                    Configuration.NeuronPerLayer, 
                    (i == 1) ? topCount : Configuration.NeuronPerLayer, 
                    (i == Configuration.HiddenLayerCount) ? bottomCount : Configuration.NeuronPerLayer
                    
                    );
            }
        }


        private void AttachInput(IInputLayer inputLayer)
        {
            int neuronPerLayer = inputLayer.Width * inputLayer.Height;

            _layers[0] = new Layer(Configuration,neuronPerLayer, 0, Configuration.NeuronPerLayer);

            inputLayer.LoadLayer(_layers[0]);
        }

        private void AttachOutput(IOutputReader<T> outputLayer,int layerPosition)
        {
            _layers[layerPosition] = new Layer(Configuration,outputLayer.OutputCount, Configuration.NeuronPerLayer, 0);

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
                    + (_inputLayer.Height * _inputLayer.Width) * Configuration.NeuronPerLayer
                    + Configuration .HiddenLayerCount* Configuration.NeuronPerLayer
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
                    layer.Neurons[j].ComputeWeight();
                }
            }
        }
    }
}
