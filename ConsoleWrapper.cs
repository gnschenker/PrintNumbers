using System;
using NumbersLibrary;

namespace PrintNumbers
{
    public class ConsoleWrapper : IOutput
    {
        public void Write(string number)
        {
            Console.WriteLine(number);
        }
    }
}