using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuronalNetwork
{
    /// <summary>
    /// Multi-layer Perceptron classifier.
    /// </summary>
    public class MLPClassifier
    {
        public List<Neuron[]> network;
        public double[] bias;
        private Random random;
        private ActivationFunction activation;
        private double totalError = 0;
        private double learningRate = 0.5;
        private readonly bool verbose;

        public MLPClassifier(
            int[] layerSizes, 
            string activation = "sigmoid", 
            int? randomSeed = null,
            bool verbose = false,
            double learningRate = 0.05)
        {
            
            network = new List<Neuron[]>();
            random = randomSeed.HasValue ? new Random(randomSeed.Value) : new Random();

            this.activation = new ActivationFunction[] { new IdentityActivation(), new LogisticActivation() }.First(x=>x.GetName() == activation);

            //Initialize weigths & bias
            network.Add(Enumerable.Range(0, layerSizes[0]).Select(i => new Neuron(random, 0)).ToArray());
            for (int i = 1; i < layerSizes.Length; i++)
            {
                network.Add(Enumerable.Range(0, layerSizes[i]).Select(_ => new Neuron(random,layerSizes[i-1])).ToArray());
            }

            bias = Enumerable.Range(0, layerSizes.Length).Select(i => random.NextDouble()).ToArray();
            this.verbose = verbose;
            this.learningRate = learningRate;
        }

        public void Fit(double[][] X, double[][] y, int numberOfEpochs = 1)
        {
            for (int e = 0; e < numberOfEpochs; e++)
            {
                for (int i = 0; i < X.Length; i++)
                {
                    Foward(X[i]);

                   
                    totalError = CalculateError(y[i]);

                    BackwardOutputLayer(y[i]);
                    BackwardHiddenLayer();
                }
                Print($"epoch: {e}. total error: {totalError}");
            }
        }

        public double[][] Predict(double[][] X)
        {
            var predictions = new List<double[]>(X.Length);
            for (int i = 0; i < X.Length; i++)
            {
                var p = Foward(X[i]);
                predictions.Add(p);
            }
            return predictions.ToArray();
        }

        private double[] Foward(double[] inputVector)
        {
            for (int i = 0; i < inputVector.Length; i++)
            {
                network[0][i].Value = inputVector[i];
            }

            for (int L = 1; L < network.Count(); L++)
            {
                for (int xi = 0; xi < network[L].Length; xi++)
                {
                    var neuron = network[L][xi];
                    double z = 0;

                    for (int xj = 0; xj < network[L-1].Length; xj++)
                    {
                        var sn = network[L - 1][xj];
                        var w = neuron.Weigths[xj];
                        var x = sn.Value;
                        z += x * w;
                    }
                    z += bias[L];
                    neuron.Value = activation.Activate(z);
                } 
            }

            return network[network.Count - 1].Select(x=>x.Value).ToArray();
        }

        private void Print(string m)
        {
            if (!verbose)
                return;
            Console.WriteLine(m);
        }

        private void BackwardOutputLayer(double[] y)
        {
            var outputLayer = network.Last();
            for (int i = 0; i < outputLayer.Length; i++)
            {
                var lastHiddenLayer = network[network.Count() - 2];
                var error = - (y[i] - outputLayer[i].Value);
                var delta = error * activation.Deactivate(outputLayer[i].Value);
                
                outputLayer[i].PartialDelta = delta;
                
                for (int j = 0; j < lastHiddenLayer.Length; j++)
                {
                    var d = delta * lastHiddenLayer[j].Value;
                    double newWeight = outputLayer[i].Weigths[j] - (learningRate * d);
                    outputLayer[i].UpdateWeight(j, newWeight);
                }
            }
        }

        private void BackwardHiddenLayer()
        {
            for (int i = network.Count() - 2; i >= 1 ; i--)
            {
                var layer = network[i];
                var outputLayer = network[network.Count - 1];
                var inputLayer = network[i - 1];
                for (int ni = 0; ni < layer.Length; ni++)
                {
                    var node = layer[ni];

                    double sumPartial = 0;
                    for (int mi = 0; mi < outputLayer.Length; mi++)
                    {
                        sumPartial += outputLayer[mi].WeigthsPrevious[ni] * outputLayer[mi].PartialDelta;
                    }

                    for (int wi = 0; wi < node.Weigths.Length; wi++)
                    {
                        var delta = sumPartial * activation.Deactivate(node.Value) * inputLayer[wi].Value;

                        double newWeight = node.Weigths[wi] - learningRate * delta;
                        node.UpdateWeight(wi, newWeight);
                    }
                }
            }
        }

        private double CalculateError(double[] y)
        {
            var aL = network[network.Count -1].Select(x=>x.Value).ToArray();
            double cost = 0;
            for (int x = 0; x < y.Length; x++)
            {
                cost += Math.Pow(Math.Abs(y[x] - aL[x]), 2) / 2;
            }
            return cost;
        }

      
    }
}
