using System.Collections.Generic;

namespace Perceptron.Core.Interfaces
{
    public interface IOutputReader<T>
    {
        int OutputCount { get; }
        T[] OuputValues { get; set; }
        List<OutputAssociation<Neuron, T>> Associations { get; }
        T ReadResult();

    }
}