using FluentAssertions;
using NUnit.Framework;
using Perceptron_Number;
using Perceptron_Number.IO;
using System;


namespace Perceptron.Tests
{
    public class NeuronTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateNeuron()
        {
            //arrange
            //act
            Action sut = () => new Neuron(0.2f);
            //assert
            sut.Should().NotThrow();
        }
        [TestCase(0.0f)]
        [TestCase(0.5f)]
        [TestCase(0.000001f)]
        [TestCase(1.0f)]
        public void CreateNeuronValue(float testValue)
        {
            //arrange
            //act
            var sut = new Neuron(testValue);
            //assert

            sut.ActivationLevel.Should().Be(testValue);
        }
        [TestCase(20,10)]
        [TestCase(2,1)]
        [TestCase(1,1)]
        public void CreateNeuronWithLinksArray(int parentCount,int childrenCount)
        {
            //arrange
            //act
            var sut = new Neuron(0.5f, parentCount, childrenCount);
            //assert

            sut.ParentsLink.Length.Should().Be(parentCount);
            sut.ChildrenLink.Length.Should().Be(childrenCount);
        }
        [TestCase(2.0f)]
        [TestCase(-1.0f)]
        [TestCase(-10.0f)]
        [TestCase(1.01f)]
        public void CreateFalsyNeuron(float testValue)
        {
            //arrange
            Action sut = () => new Neuron(testValue);
            //act
            //assert
            sut.Should().Throw<ArgumentOutOfRangeException>();
            
            //.WithMessage("whatever");
        }
        [Test]
        public void CreateMaxDefaultNeuron()
        {
            //arrange
            var sut = Neuron.MaxDefault();

            //act
            //assert
            sut.ActivationLevel.Should().Be(1.0f);
        }
        [Test]
        public void CreateMinDefaultNeuron()
        {
            //arrange
            var sut = Neuron.MinDefault();

            //act
            //assert
            sut.ActivationLevel.Should().Be(0.0f);
        }
        [TestCase(1.0f,1.0f,true)]
        [TestCase(0.0f,0.0f, true)]
        [TestCase(1.0f, 0.0f, false)]
        [TestCase(0.0f, 1.0f, false)]
        [TestCase(0.5f, 0.5f, true)]
        [TestCase(0.0005f, 0.0005f, true)]
        [TestCase(0.0001f, 0.00009f, false)]
        public void NeuronEquality(float firstValue, float secondValue, bool equality)
        {
            //arrange
            var sut1 = new Neuron(firstValue);
            var sut2 = new Neuron(secondValue);

            //act

            //assert
            sut1.Equals(sut2).Should().Be(equality);
        }
        [TestCase(0.0f)]
        [TestCase(0.5f)]
        [TestCase(0.000001f)]
        [TestCase(1.0f)]
        public void NeuronToString(float testValue)
        {
            //arrange
            var sut = new Neuron(testValue);
            //act
            string outString = sut.ToString();
            //assert
            outString.Should().Be($"w:{testValue}");

        }
        [Test]
        public void NeuronLink()
        {
            /*
            //arrange
            var sut = new Neuron(0.2f,10,10);
            //act
            //sut.
            //assert
            outString.Should().Be($"w:{testValue}");*/

        }
        [TestCase(0.0f)]
        [TestCase(0.5f)]
        [TestCase(-10f)]
        [TestCase(20.0f)]
        public void NeuronBias(float testValue)
        {
            //arrange
            var sut = new Neuron(1);

            //act
            sut.Bias = testValue;
            //assert

            sut.Bias.Should().Be(testValue);
        }

    }
}
