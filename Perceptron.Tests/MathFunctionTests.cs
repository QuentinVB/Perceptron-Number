using FluentAssertions;
using NUnit.Framework;
using Perceptron.Core;
using Perceptron.Core.IO;
using System;


namespace Perceptron.Tests
{
    public class MathFunctionTests
    {
        [SetUp]
        public void Setup()
        {
        }



        [TestCase(-5f,0)]
        [TestCase(-1f,0)]
        [TestCase(-0.01f,0)]
        [TestCase(0f,0.5f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 0)]
        [TestCase(5f, 0)]
        public void TestSigmoid(float x,float y)
        {
            //arrange
            //act
            var sut = Neuron.Sigmoid(x);
            //assert

            sut.Should().BeApproximately(y, 0.001F);
        }

        [TestCase(-5f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0.01f)]
        [TestCase(1f, 1f)]
        [TestCase(5f, 5f)]
        public void TestReLU(float x, float y)
        {
            //arrange
            //act
            var sut = Neuron.ReLU(x);
            //assert

            sut.Should().BeApproximately(y, 0.001F);
        }

        [TestCase(-5f, -0.005f)]
        [TestCase(-1f, -0.001f)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0.01f)]
        [TestCase(1f, 1f)]
        [TestCase(5f, 5f)]
        public void TestLeakyReLU(float x, float y)
        {
            //arrange
            //act
            var sut = Neuron.LeakyReLU(x);
            //assert

            sut.Should().BeApproximately(y, 0.0001F);
        }

        [TestCase(-5f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 1f)]
        [TestCase(5f, 1f)]
        public void TestNormalize(float x, float y)
        {
            //arrange
            //act
            var sut = Neuron.Normalize(x);
            //assert

            sut.Should().BeApproximately(y, 0.01F);
        }  
    }
}
