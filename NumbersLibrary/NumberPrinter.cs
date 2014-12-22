using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersLibrary
{
    public class NumberPrinter : INumberPrinter
    {
        private readonly IOutput outputStream;
        readonly Dictionary<int, string> specialNumbers = new Dictionary<int, string>();

        public NumberPrinter(IOutput outputStream)
        {
            this.outputStream = outputStream;
        }

        public void PrintNumbers(int lower = 1, int upper = 100)
        {
            if(lower<=0) throw new ArgumentException("Only positive lower bounds allowed");
            if(lower>=upper) throw new ArgumentException("Lower bound must be smaller than upper bound");

            var reverted = specialNumbers.Reverse();
            for (var i = lower-1; i < upper; i++)
            {
                var found = false;
                var number = i + 1;
                foreach (var pair in reverted)
                {
                    if (number%pair.Key == 0)
                    {
                        outputStream.Write(pair.Value);
                        found = true;
                        break;
                    }
                }
                if(!found)
                    outputStream.Write(number.ToString());
            }
        }

        public void Register(int divisor, string outputText)
        {
            if(divisor<=0) throw new ArgumentException("Only divisors greater than zero are allowed");
            if(string.IsNullOrWhiteSpace(outputText)) throw new ArgumentException("Output text must be a non-null string");

            specialNumbers[divisor] = outputText;
        }
    }
}