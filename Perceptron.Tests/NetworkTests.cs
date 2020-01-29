using FluentAssertions;
using NUnit.Framework;
using Perceptron.Core;
using Perceptron.Core.Configurations;
using Perceptron.Core.Interfaces;
using Perceptron.Core.IO;
using System;

namespace Perceptron.Tests
{
    public class NetworkTests
    {
        //TODO: should test the input/output
        Input _input ;
        Output<int> _output;
        IConfiguration commonConfig;

        [SetUp]
        public void Setup()
        {
            _input = Input.DefaultInput();
            _output = Output<int>.DefaultOutput(default(int));
            commonConfig = new BasicConfiguration();
        }

        [Test]
        public void NetworkCreation()
        {
            //arrange
            Action sut = () => new Network<int>(commonConfig, _input,_output);
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
            IConfiguration testConfig = new TestableConfiguration(hiddenLayerCount, neuronPerLayer);
            //Act
            Action sut = () => new Network<int>(testConfig, _input, _output);
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [TestCase(2, 2)]
        [TestCase(3, 16)]
        public void NetworkLayerCount(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            IConfiguration testConfig = new TestableConfiguration(hiddenLayerCount, neuronPerLayer);
            //Act
            Network<int> sut = new Network<int>(testConfig, _input, _output);
            //assert
            sut.Layers.Length.Should().Be(hiddenLayerCount+2);
        }
        [TestCase(2, 2)]
        public void NetworkTotalNeuronCount(int hiddenLayerCount, int neuronPerLayer)
        {
            //arrange
            IConfiguration testConfig = new TestableConfiguration(hiddenLayerCount, neuronPerLayer);
            //Act
            Network<int> sut = new Network<int>(testConfig, _input, _output);
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
            Network<int> sut = new Network<int>(commonConfig, _input, _output);
            //Act

            //assert
            sut.ReadResult().Should().Be(0);
        }
    }
}