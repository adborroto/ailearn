namespace AILearn.Core
{
    public class ClasificationReport
    {
        private readonly double threshold;

        public ClasificationReport(double[][] test, double[][] predictions, double threshold = 0.50)
        {
            Test = test;
            Predictions = predictions;
            this.threshold = threshold;
            CalculateReport();
        }

        private void CalculateReport()
        {
            int correct = 0;

            for (int i = 0; i < Test.Length; i++)
            {
                var test = Test[i];
                var prediction = Predictions[i];

                var badPrediction = false;
                for (int x = 0; x < test.Length; x++)
                {
                    //Positive case
                    if (test[x] >= threshold)
                    {
                        if (prediction[x] >= threshold)
                            continue;
                        else
                            badPrediction = true;
                    }
                    //Negative case
                    else
                    {
                        if (prediction[x] >= threshold)
                            badPrediction = true;
                        else
                            continue;
                    }
                }
                if (!badPrediction)
                    correct++;

            }
            CorrectPredictions = correct;
            SampleLength = Predictions.Length;
            IncorrectPredictions = correct - SampleLength;
        }

        public int CorrectPredictions { get; set; }

        public int SampleLength { get; set; }

        public int IncorrectPredictions { get; set; }

        public double[][] Test { get; }
        public double[][] Predictions { get; }
    }
}
