namespace NeuronalNetwork
{
    //identity’, ‘logistic’, ‘tanh’

    /// <summary>
    /// In mathematics, an identity function, also called an identity relation or identity map or identity transformation, 
    /// is a function that always returns the same value that was used as its argument. 
    /// That is, for f being identity, the equality f(x) = x holds for all x.
    /// </summary>
    public class IdentityActivation : ActivationFunction
    {
        public override double Activate(double value)
        {
            return value;
        }

        public override double Deactivate(double value)
        {
            return 1;
        }

        public override string GetName()
        {
            return "identity";
        }
    }
}