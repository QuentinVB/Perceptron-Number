namespace Perceptron.Core
{
    public struct OutputAssociation<Neuron,T>
    {
        public T ValueOut { get; }
        public Neuron LinkedNeuron { get; }
        public OutputAssociation(T valueOut, Neuron neuron)
        {
            ValueOut = valueOut;
            LinkedNeuron = neuron;
        }
    }
}