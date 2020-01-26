using System.Collections.Generic;

namespace Perceptron_Number.Interfaces
{
    public interface IInputLayer
    {
        int Width { get; }
        int Height { get; }

        Layer InputLayer { get; }
        void LoadLayer(Layer layer);

        void SetNeuronValueFrom(int x, int y);
    }
}