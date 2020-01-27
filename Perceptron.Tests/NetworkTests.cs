using FluentAssertions;
using NUnit.Framework;
using Perceptron.Core;
using Perceptron.Core.IO;
using System;

namespace Perceptron.Tests
{
    public class NetworkTests
    {
        //TODO: should test the input/output
        Input _input ;
        Output<int> _output;

        [SetUp]
        public void Setup()
        {
            _input = Input.DefaultInput();
            _output = Output<int>.DefaultOutput(default(int));
        }

        [TestCase(1,1)]
        [TestCase(2, 2)]
        [TestCase(3,5)]
        public void NetworkCreation(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            Action sut = () => new Network<int>(hiddenLayerCount, neuronPerLayer, _input,_output);
            //Act

            //assert
            sut.Should().NotThrow();
        }

        [TestCase(-2, 2)]
        [TestCase(2, -2)]
        [TestCase(-2, -2)]
        [TestCase(2, 0)]
        [TestCase(0, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, 0)]
        [TestCase(0, -2)]
        public void FalsyNetworkCreation(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            Action sut = ()=> new Network<int>(hiddenLayerCount, neuronPerLayer, _input, _output);
            //Act
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [TestCase(2, 2)]
        [TestCase(3, 16)]
        public void NetworkLayerCount(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            Network<int> sut = new Network<int>(hiddenLayerCount, neuronPerLayer, _input, _output);
            //Act

            //assert
            sut.Layers.Length.Should().Be(hiddenLayerCount+2);
        }
        [TestCase(2, 2)]
        public void NetworkTotalNeuronCount(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            Network<int> sut = new Network<int>(hiddenLayerCount, neuronPerLayer, _input, _output);
            //Act

            //assert
            sut.NeuronCount.Should().Be(
                (hiddenLayerCount* neuronPerLayer) 
                + (_input.Width*_input.Height)
                + (_output.OutputCount)
                );
        }
        [Test]
        
        public void NetworkReadResult()
        {
            //arrange
            Network<int> sut = new Network<int>(2, 2, _input, _output);
            //Act

            //assert
            sut.ReadResult().Should().Be(0);
        }
    }
}