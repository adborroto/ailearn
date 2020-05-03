using AILearn.Core;
using NeuronalNetwork;
using System.Linq;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var df = DataFrame.ReadCSV("iris.csv", ',', true);
            var X = df.GetDoubleRange(0, 4);
            var y = df.GetRange(4, 5, x => x).Select(x => x[0]).ToArray();
            var Y = DataFrame.GetDummies(y);


            var testSplit = new TrainTestSplit(X, Y);
            var t = testSplit.Split(0, testSize: 0.30);

            var network = new MLPClassifier(new int[] { 4, 5, 5, 5, 3 },
                                            activation: "sigmoid",
                                            randomSeed: 0,
                                            verbose: true,
                                            learningRate: 0.5);

            network.Fit(t.XTrain, t.YTrain, numberOfEpochs: 200);
            var predictions = network.Predict(t.XTest);

            var report = new ClasificationReport(t.YTest, predictions);
            System.Console.WriteLine($"Good predictions: {report.CorrectPredictions}/{report.SampleLength}. Presicion: {report.CorrectPredictions / (double)report.SampleLength}");

            System.Console.ReadLine();
        }
    }
}
