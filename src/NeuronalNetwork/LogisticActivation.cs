namespace NeuronalNetwork
{
    /// <summary>
    /// A logistic function or logistic curve is a common S-shaped curve (sigmoid curve) with equation
    /// </summary>
    public class LogisticActivation : ActivationFunction
    {
        public override double Activate(double value)
        {
            return 1.0 / (1 + System.Math.Pow(System.Math.E, -value));
        }

        public override double Deactivate(double value)
        {
            return value * (1 - value);
        }

        public override string GetName()
        {
            return "sigmoid";
        }
    }
}