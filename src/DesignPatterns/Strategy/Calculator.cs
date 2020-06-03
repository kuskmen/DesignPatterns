namespace DesignPatterns.Strategy
{
    // Just an example
    public class Calculator<T>
    {

        public Calculator(IOperationStrategy<T> addition, IOperationStrategy<T> substraction, IOperationStrategy<T> division)
        {
            Addition = addition;
            Substraction = substraction;
            Division = division;
        }

        public IOperationStrategy<T> Addition { get; }
        public IOperationStrategy<T> Substraction { get; }
        public IOperationStrategy<T> Division { get; }

        public T Operate(Operation operate, T left, T right)
        {
            switch (operate)
            {
                case Operation.Addition:
                    return Addition.Operate(left, right);
                case Operation.Substraction:
                    return Substraction.Operate(left, right);
                case Operation.Division:
                    return Division.Operate(left, right);
                default:
                    return default;
            }
        }

        public enum Operation : byte
        {
            Addition = 1,
            Substraction = 2,
            Division = 3,
        }
    }
}
