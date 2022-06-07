using System;
using System.Collections.Generic;
using CalculatorLib;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            if ( args.Length < 1 )
            {
                Console.WriteLine( "Please enter calculation string" );
            }
            List<IOperation> operations = new()
            {
                new AdditionOperation(),
                new SubstractionOperation(),
                new MultiplicationOperation(),
                new DivisionOperation()
            };
            ICalculator calculator = new SimpleCalculator( operations );
            calculator.Calculate( args[0] );
        }
    }
}
