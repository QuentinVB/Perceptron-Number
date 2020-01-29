using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Perceptron.Core;
using Perceptron.Core.Interfaces;
using Perceptron.Core.Configurations;

namespace Perceptron.Tests
{
    public class LayerTests
    {
        IConfiguration config;
        [SetUp]
        public void Setup()
        {
            config = new BasicConfiguration();
        }
        [Test]
        public void CreateLayer()
        {
            //arrange
            Action sut = () => new Layer(config,2);
            //act
            //assert

            sut.Should().NotThrow();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(40)]
        public void CreateLayerValue(int testValue)
        {
            //arrange
            var sut = new Layer(config,testValue);
            //act
            //assert

            sut.Neurons.Length.Should().Be(testValue);
        }
        [TestCase(-1,1,1)]
        [TestCase(1,-1, 1)]
        [TestCase(1,1, -1)]
        [TestCase(0,1,1)]
        public void CreateFalsyLayer(int neuronCount,int topNeuronCount, int bottomNeuronCount)
        {
            Action sut = () => new Layer(config,neuronCount, topNeuronCount,bottomNeuronCount);
            //act
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [Test]
        public void LayerNeuronInitialization()
        {
            Layer sut = new Layer(config,20);
            //act
            //assert
            foreach (Neuron neuron in sut.Neurons)
            {
                neuron.ActivationLevel.Should().Be(0.0f);
            }
        }
    }
}
