using System.Collections.Generic;

namespace Perceptron.Core.Interfaces
{
    public interface IInputLayer
    {
        int Width { get; }
        int Height { get; }

        Layer InputLayer { get; }
        void LinkLayer(Layer layer);

        void UpdateLayer(float[,] newMatrix);

        void SetNeuronValueFrom(int x, int y);
    }
}