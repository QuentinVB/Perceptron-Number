using FluentAssertions;
using NUnit.Framework;
using Perceptron.Core;
using Perceptron.Core.IO;
using System;


namespace Perceptron.Tests
{
    public class LinkTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateLink()
        {
            //arrange
            //act
            Action sut = () => new Link();
            //assert
            sut.Should().NotThrow();
        }

        [Test]
        public void BindTwoNeuron()
        {
            //arrange
            Neuron neuron1 = new Neuron(0.5f,0,1);
            Neuron neuron2 = new Neuron(0.3f,1,0);
            //act
            var sut =  new Link(neuron1, neuron2,0.5f);
            //assert
            sut.InputNeuron.Should().Be(neuron1);
            sut.OutputNeuron.Should().Be(neuron2);
        }

        [TestCase(0.5f,2)]
        public void PonderateWeightNeuron(float neuronActivation,float linkWeight)
        {
            //arrange
            Neuron neuron1 = new Neuron(neuronActivation, 0, 1);
            Neuron neuron2 = new Neuron(0.3f, 1, 0);
            //act
            var sut = new Link(neuron1, neuron2, linkWeight);
            //assert
            sut.PonderateWeight.Should().Be(neuronActivation * linkWeight);
        }


    }
}
