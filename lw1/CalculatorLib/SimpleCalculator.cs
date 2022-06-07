using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class SimpleCalculator : ICalculator
    {
        private List<IOperation> _supportedOperations;
        public SimpleCalculator( List<IOperation> operations ) 
        {
            _supportedOperations = operations;
        }
        public int Calculate( string inputString )
        {
            string[] numbersAndOperations = Regex
                .Replace( inputString, @"([\d]+)", "'$1'" )
                .Split( new[] { '\'' }, StringSplitOptions.RemoveEmptyEntries );

            int result = int.Parse( numbersAndOperations[0] );
            for ( int i = 1; i < numbersAndOperations.Length; i += 2 )
            {
                string operationCode = numbersAndOperations[i].Trim();
                IOperation operation = _supportedOperations.Find(x => x.OperatorCode == operationCode);
                int number = int.Parse(numbersAndOperations[i + 1]);
                result = operation.Apply(result, number);
            }
            return result;
        }
    }
}
