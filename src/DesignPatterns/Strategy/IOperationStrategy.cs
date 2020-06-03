namespace DesignPatterns.Strategy
{
    public interface IOperationStrategy<T>
    {
        T Operate(T left, T right);
    }

    public class IntegerAddition : IOperationStrategy<int>
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }

    public class IntegerSubstraction : IOperationStrategy<int>
    {
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }

    public class IntegerDivision : IOperationStrategy<int>
    {
        public int Operate(int left, int right)
        {
            // loosing precision, dont care its just a demo.s
            return right == 0 ? right : left / right;
        }
    }

    // more operations for different types
}
