using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Perceptron.Tests")]
namespace Perceptron_Number
{
    public class Neuron
    {
        float _value; //activation level
        float _bias=0; //activation level
        int _parentIndex=0;
        Link[] _parentsLink;
        int _childrenIndex=0;
        Link[] _childrenLink;


        public Neuron(float value):
            this(value,0,0)
        {  }


        public Neuron(float value,int parentsLinkCount, int childrenLinkCount)
        {
            if (0 > value || value > 1) throw new ArgumentOutOfRangeException("Activation level should be between 0 and 1. Was " + value);
            if (parentsLinkCount<0) throw new ArgumentOutOfRangeException("parentLink Count should be positive");
            if (childrenLinkCount < 0) throw new ArgumentOutOfRangeException("childrenLink Count should be positive");
            _value = value;

            _parentsLink = new Link[parentsLinkCount];
            _childrenLink = new Link[childrenLinkCount];
        }

        ///activation level
        public float ActivationLevel { 
            get => _value; 
            set {
                if ( 0> value || value > 1) throw new ArgumentOutOfRangeException("Activation level should be between 0 and 1. Was " + value);
                _value = value; } 
        }

        public Link[] ParentsLink { get => _parentsLink; set => _parentsLink = value; }
        public Link[] ChildrenLink { get => _childrenLink; set => _childrenLink = value; }
        public float Bias { get => _bias; set => _bias = value; }

        public static Neuron MaxDefault()
        {
            return new Neuron(1.0f);
        }
        public static Neuron MinDefault()
        {
            return new Neuron(0.0f);
        }

        public override bool Equals(object obj)
        {
            return obj is Neuron neuron &&
                   _value == neuron._value;
        }

        public override string ToString()
        {
            return $"w:{_value}";
        }

        internal void AddParent(Link link)
        {
            _parentIndex=AddLink(link, _parentIndex, ParentsLink);
        }

        internal void AddChildren(Link link)
        {
            _childrenIndex=AddLink(link, _childrenIndex, ChildrenLink);
        }
        private int AddLink(Link link,int index, Link[] links)
        {
            if (index + 1 > links.Length) throw new ArgumentOutOfRangeException();

            links[index++] = link;

            return index;
        }

        private float SumParents()
        {
            float sum = 0.0f;
            for (int i = 0; i < ParentsLink.Length; i++)
            {
                sum += ParentsLink[i].PonderateWeight;
            }
            return sum;
        }

        //TODO: sigmoid is old School !
        /*ReLU : rectified Linear Unit : learning will be faster
         */
        public static float sigmoid(float x)
        {
            return 1f / 1f + (float)Math.Pow(Math.E,-x);
        }
        public static float ReLU(float x)
        {
            if (x < 0) return 0;
            return x;
        }
        public static float LeaxyReLU(float x)
        {
            if (x < 0) return x*0.001f;
            return x;
        }

        public static float Normalize(float x)
        {
            if (x < 0) return 0;
            else return Math.Max(0, x);
        }

        public float ComputeWeight()
        {
            return sigmoid(SumParents() + _bias);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }
}
