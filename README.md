# ailearn

Machine learning algorithms for learning purpuses.

# Multi-layer Perceptron (MLP)

Example using [iris](https://archive.ics.uci.edu/ml/datasets/iris) dataset

```charp

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

```