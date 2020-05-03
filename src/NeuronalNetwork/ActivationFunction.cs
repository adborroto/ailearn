namespace NeuronalNetwork
{
    public abstract class ActivationFunction
    {
        public abstract double Activate(double value);

        public abstract double Deactivate(double value);

        public abstract string GetName();
    }
}