using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Perceptron.Core;
using Perceptron.ImageInput;
using Perceptron.Core.IO;
using Perceptron.Core.Interfaces;
using Perceptron.Core.Configurations;

namespace Perceptron.Tests
{
    public class InputTests
    {
        float[,] matrix;
        int width=20;
        int heigth=20;
        Layer fakelayer;
        IConfiguration config;

        [SetUp]
        public void Setup()
        {
            matrix = SourceImage.GetBWMatrixFromPicture("1579480335.png");
            config = new BasicConfiguration();
            fakelayer = new Layer(config,width * heigth);
        }
        [Test]
        public void CreateInput()
        {
            //arrange
            Action sut = () => new Input(width,heigth,matrix);
            //act
            //assert
            sut.Should().NotThrow();
        }
        [Test]
        public void CreateInputValue()
        {
            //arrange
            var sut = new Input(width, heigth, matrix);
            //act
            //assert
            sut.Width.Should().Be(width);
            sut.Height.Should().Be(heigth);
            sut.Matrix.Length.Should().Be(heigth* width);
        }
        [TestCase(-2, 2)]
        [TestCase(2, -2)]
        [TestCase(-2, -2)]
        [TestCase(2, 0)]
        [TestCase(0, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, 0)]
        [TestCase(0, -2)]
        public void CreateFalsyInput(int w,int h)
        {
            Action sut = () => new Input(w, h, matrix);
            //act
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [TestCase(20, 21)]
        [TestCase(10, 10)]
        public void BindFalsyMatrixArguments(int w, int h)
        {
            Action sut = () => new Input(w, h, matrix);
            //act
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [TestCase(20, 21)]
        [TestCase(10, 10)]
        public void BindFalsyMatrix(int w, int h)
        {
            Action sut = () => new Input(width, heigth, new float[w,h]);
            //act
            //assert
            sut.Should().Throw<ArgumentException>();
        }
        [Test]
        public void LayerNeuronInitialization()
        {
            //arrange
            Input sut = new Input(width, heigth, matrix);
            //act
            sut.LoadLayer(fakelayer);
            //assert
            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    fakelayer.Neurons[x + y * width].ActivationLevel.Should().BeApproximately(matrix[x, y], 0.001F);
                }
            }
        }
        [Test]
        public void FalsyLayerNeuron()
        {
            //arrange
            Input input = new Input(width, heigth, matrix);
            Layer x=null;
            Action sut = () => x=input.InputLayer;
            //act
            //assert
            sut.Should().Throw<InvalidOperationException>();
        }

    }
}
