using System;
using NumbersLibrary;

namespace PrintNumbers
{
    //*********************************************************************
    // Since the NumberPrinter is supposed to be reusable I packaged it in
    // its own library called NumbersLibrary. I also defined an interface
    // for the NumberPrinter such that developers using this component can
    // mock it if needed.
    //*********************************************************************

    class Program
    {
        static void Main()
        {
            var numberPrinter = new NumberPrinter(new ConsoleWrapper());
            numberPrinter.Register(4, "              ****************!");
            numberPrinter.Register(5, "               Merry Christmas");
            numberPrinter.Register(6, "                     and");
            numberPrinter.Register(7, "              a Happy New Year!");
            numberPrinter.PrintNumbers(upper:30);

            Console.WriteLine();
            Console.Write("Hit enter to exit:");
            Console.ReadLine();
        }
    }
}
