namespace AILearn.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TrainTestSplit
    {
        public TrainTestSplit(double[][] X, double[][] Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double[][] X { get; }
        public double[][] Y { get; }

        public SplitResult Split(int? random = null, double testSize = 0.33)
        {
            Random r = new Random();
            if (random.HasValue)
                r = new Random(random.Value);

            var trainSize = (int)(X.Length * (1 - testSize));

            var bucket = new List<Tuple<double[], double[], double>>();
            for (int i = 0; i < X.Length; i++)
            {
                bucket.Add(new Tuple<double[], double[], double>(X[i], Y[i], r.NextDouble()));
            }
            var bucketTrain = bucket.OrderBy(x => x.Item3).Take(trainSize);
            var bucketTest = bucket.OrderBy(x => x.Item3).Skip(trainSize);

            var result = new SplitResult
            {
                XTrain = bucketTrain.Select(x => x.Item1).ToArray(),
                XTest = bucketTest.Select(x => x.Item1).ToArray(),
                YTest = bucketTest.Select(x => x.Item2).ToArray(),
                YTrain = bucketTrain.Select(x => x.Item2).ToArray()
            };

            return result;
        }

        public class SplitResult
        {
            public double[][] XTrain { get; set; }
            public double[][] XTest { get; set; }
            public double[][] YTrain { get; set; }
            public double[][] YTest { get; set; }
        }
    }

}
