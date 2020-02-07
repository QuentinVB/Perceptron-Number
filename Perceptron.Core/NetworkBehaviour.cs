using Perceptron.Core.Interfaces;
using Perceptron.ImageInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron.Core
{
    public enum NetworkMode
    {
        Learning,
        Testing,
        Production
    }
    public partial class Network<T>
    {
        float _accuracy;
        NetworkMode _mode;
        public NetworkMode Mode { get => _mode; internal set => _mode = value; }


        public void UpdateNetwork(byte[] newMatrix,int width,int height)
        {
            if( width != InputLayer.Width || height != InputLayer.Height) throw new DataMisalignedException("");

            float[,] floatMatrix = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (newMatrix[i + j * width] > 0) 
                    { 
                        floatMatrix[i, j] = (newMatrix[i + j * width] / 255f);
                    }
                    
                }
            }
            UpdateNetwork(floatMatrix);
        }
        public void UpdateNetwork(float[,] newMatrix)
        {
            InputLayer.UpdateLayer(newMatrix);
            UpdateWeight();
        }

        public float TestingNetworkAccuracy(ImageDataBlock[] imageDataBlocks,int imageCount)
        {
            Mode = NetworkMode.Testing;

            if (imageDataBlocks[0].CommonInfo.Width != InputLayer.Width || imageDataBlocks[0].CommonInfo.Height != InputLayer.Height) throw new DataMisalignedException("");

            float accuracy = 0f;

            for (int i = 0; i < imageCount; i++)
            {
                UpdateNetwork(imageDataBlocks[i].Image, imageDataBlocks[i].CommonInfo.Width, imageDataBlocks[i].CommonInfo.Height);
                var result = OutputLayer.ReadResult();
                //not generic
                if(result is int computedResult)
                {
                    if (computedResult == imageDataBlocks[i].Label) accuracy++;
                }
            }
            _accuracy = accuracy / imageCount;
            return _accuracy;
        }


        public float NetworkLearning(ImageDataBlock[] imageDataBlocks, int imageCount)
        {
            Mode = NetworkMode.Learning;

            if (imageDataBlocks[0].CommonInfo.Width != InputLayer.Width || imageDataBlocks[0].CommonInfo.Height != InputLayer.Height) throw new DataMisalignedException("");

            float accuracy = 0f;

            for (int i = 0; i < imageCount; i++)
            {
                UpdateNetwork(imageDataBlocks[i].Image, imageDataBlocks[i].CommonInfo.Width, imageDataBlocks[i].CommonInfo.Height);
                var result = OutputLayer.ReadResult();
                //not generic
                if (result is int computedResult)
                {
                    if (computedResult == imageDataBlocks[i].Label) accuracy++;
                    else
                    {
                        //float cost = OutputLayer.ComputeCost(imageDataBlocks[i].Label);
                    }
                }
            }
            _accuracy = accuracy / imageCount;
            return _accuracy;
        }

    }
}
