using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Core.Interfaces
{
    public interface IConfiguration
    {
        Random RandomSource { get; }
        int HiddenLayerCount { get;}
        int NeuronPerLayer { get; }
        float BiasSource { get;  }
        float WeightSource { get;  }
        float ActivationFunction(float x);
    }
}
