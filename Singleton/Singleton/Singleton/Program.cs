using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            StandartSingleton ss = StandartSingleton.GetInstance();
 

            Console.WriteLine(ss);
            Console.WriteLine(StandartSingletonv2.Instance);
            
        }
    }
}
