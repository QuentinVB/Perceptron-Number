﻿using Perceptron.Core;
using Perceptron.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core.IO
{
    public class Input : IInputLayer
    {
        Layer _inputLayer;
        readonly int _width;
        readonly int _height;
        float[,] _matrix;

        public Input(int width, int height):this(width, height,new float[width , height])
        {
            
        }
        public Input(int width, int height, float[,] matrix)
        {
            if(matrix.Length != width * height) throw new ArgumentException("matrix and matrix size should be the same");

            _width = width;
            _height = height;
            _matrix = matrix;
        }
        public int Width => _height;

        public int Height => _width;
        public float[,] Matrix { get => _matrix; }

        public Layer InputLayer
        {
            get
            {
                if(_inputLayer == null) throw new InvalidOperationException("Input layer is null, LoadLayer should be called first");
                return _inputLayer;
            }
        }

        public void LinkLayer(Layer inputLayer)
        {
            if(inputLayer.Neurons.Length != Width * Height) throw new ArgumentException("input Layer should have the right number of neurons");
            _inputLayer = inputLayer;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetNeuronValueFrom(x, y);
                }
            }
        }

        public void UpdateLayer(float[,] newMatrix)
        {
            if (newMatrix.Length != Width * Height) throw new ArgumentException("input matrix should have the same width and height");
            _matrix = newMatrix;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetNeuronValueFrom(x, y);
                }
            }


        }


        /// <summary>
        /// set the value from the matrix to the corresponding neuron in first layer
        /// </summary>
        /// <param name="x">position on horizontal axis</param>
        /// <param name="y">position on vertical axis</param>
        public void SetNeuronValueFrom(int x, int y)
        {
            if(_matrix == null) throw new ArgumentNullException("Source matrix is null");
            if(_inputLayer == null) throw new InvalidOperationException("Input layer is null, should be set beforce calling.");

            int index = x + y * Width;//index in layer neuron corresponding to the x,y
            _inputLayer.Neurons[index].ActivationLevel = WeightCheck(_matrix[x, y]);
        }

        private float WeightCheck(float source)
        {
            if(source < 0 || source > 1) throw new ArgumentOutOfRangeException("Weight should be between 0 and 1. Was "+source);
            return source;
        }

        public static Input DefaultInput()
        {
            int width = 2;
            int height = 2;
            float[,] matrix = new float[width, height];
            float value = 1;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = value;
                    value /= 2;
                }
            }
            return new Input(width, height, matrix);
        }

        public static Input RandomInput()
        {
            int width = 2;
            int height = 2;
            float[,] matrix = GetRandomMatrix(width, height);
            return new Input(width, height, matrix);
        }

        public static float[,] GetRandomMatrix(int width = 2, int height = 2)
        {
            Random rnd = new Random();
            float[,] matrix = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = (float)rnd.NextDouble();
                }
            }
            return matrix;
        }
    }
}
