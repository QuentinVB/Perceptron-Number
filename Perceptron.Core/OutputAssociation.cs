namespace Perceptron.Core
{
    public struct OutputAssociation<Neuron,T>
    {
        readonly T _associatedValue;
        readonly Neuron _neuron;

        public T ValueOut { get => _associatedValue; }

        public Neuron LinkedNeuron => _neuron;

        public OutputAssociation(T valueOut, Neuron neuron)
        {
            _associatedValue = valueOut;
            _neuron = neuron;
        }
    }
}