using System.Diagnostics;
using DesignPatterns.Strategy;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator<int>(new IntegerAddition(), new IntegerSubstraction(), new IntegerDivision());

            var five = calc.Operate(Calculator<int>.Operation.Addition, 2, 3);
            Debug.Assert(five == 5);

            var minusOne = calc.Operate(Calculator<int>.Operation.Substraction, 2, 3);
            Debug.Assert(minusOne == -1);

            var zero = calc.Operate(Calculator<int>.Operation.Division, 2, 3);
            Debug.Assert(zero == 0);

        }
    }
}
