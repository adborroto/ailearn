using System;
using System.Linq;

namespace NeuronalNetwork
{
    public class Neuron
    {
        public Neuron(Random random, int d)
        {
            Weigths = Enumerable.Range(0,d).Select(x=>random.NextDouble()).ToArray();
            WeigthsPrevious = Enumerable.Range(0,d).Select(x=>random.NextDouble()).ToArray();
        }
        public double[] Weigths { get; set; }
        public double[] WeigthsPrevious { get; set; }

        public void UpdateWeight(int position, double newValue)
        {
            WeigthsPrevious[position] = Weigths[position];
            Weigths[position] = newValue;
        }

        public double Value { get; set; }

        public double PartialDelta{ get; set; }

    }
}
