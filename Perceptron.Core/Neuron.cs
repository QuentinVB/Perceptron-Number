using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Perceptron.Tests")]
namespace Perceptron.Core
{
    public class Neuron
    {
        float _activationLevel; //activation level
        float _bias=0; //bias
        int _parentIndex=0;
        Link[] _parentsLink;
        int _childrenIndex=0;
        Link[] _childrenLink;


        public Neuron(float activationLevel) :
            this(activationLevel, 0,0)
        {  }


        public Neuron(float activationLevel,int parentsLinkCount, int childrenLinkCount)
        {
            if (0 > activationLevel || activationLevel > 1) throw new ArgumentOutOfRangeException("Activation level should be between 0 and 1. Was " + activationLevel);
            if (parentsLinkCount<0) throw new ArgumentOutOfRangeException("parentLink Count should be positive");
            if (childrenLinkCount < 0) throw new ArgumentOutOfRangeException("childrenLink Count should be positive");
            _activationLevel = activationLevel;

            _parentsLink = new Link[parentsLinkCount];
            _childrenLink = new Link[childrenLinkCount];
        }

        ///activation level
        public float ActivationLevel { 
            get => _activationLevel; 
            set {
                if ( 0> value || value > 1) throw new ArgumentOutOfRangeException("Activation level should be between 0 and 1. Was " + value);
                _activationLevel = value; } 
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
            return obj is Neuron neuron 
                && _activationLevel == neuron.ActivationLevel
                && _bias == neuron.Bias;
        }

        public override string ToString()
        {
            return $"a:{_activationLevel},b:{_bias}";
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
        public static float Sigmoid(float x)
        {
            return 1f / 1f + (float)Math.Pow(Math.E,-x);
        }
        public static float ReLU(float x)
        {
            return (x < 0)?0: x;
        }
        public static float LeakyReLU(float x)
        {
            return (x < 0)? x * 0.001f : x;
        }

        public static float Normalize(float x)
        {
            return (x < 0)?0:Math.Max(0, x);
        }

        public void ComputeWeight()
        {
            _activationLevel = Sigmoid(SumParents() + _bias); 
        }

        public override int GetHashCode()
        {
            var hashCode = -1166464649;
            hashCode = hashCode * -1521134295 + _activationLevel.GetHashCode();
            hashCode = hashCode * -1521134295 + _bias.GetHashCode();
            return hashCode;
        }
    }
}
