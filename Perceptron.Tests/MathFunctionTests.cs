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
        [TestCase(0f,0f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 0)]
        [TestCase(5f, 0)]
        public void TestSigmoid(float x,float y)
        {
            //arrange
            //act
            var sut = Neuron.Sigmoid(x);
            //assert

            sut.Should().BeApproximately(y, 0.01F);
        }

        [TestCase(-5f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 0)]
        [TestCase(5f, 0)]
        public void TestReLU(float x, float y)
        {
            //arrange
            //act
            var sut = Neuron.ReLU(x);
            //assert

            sut.Should().BeApproximately(y, 0.01F);
        }

        [TestCase(-5f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 0)]
        [TestCase(5f, 0)]
        public void TestLeakyReLU(float x, float y)
        {
            //arrange
            //act
            var sut = Neuron.LeakyReLU(x);
            //assert

            sut.Should().BeApproximately(y, 0.01F);
        }

        [TestCase(-5f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(-0.01f, 0)]
        [TestCase(0f, 0f)]
        [TestCase(0.01f, 0)]
        [TestCase(1f, 0)]
        [TestCase(5f, 0)]
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
