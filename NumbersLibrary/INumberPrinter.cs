namespace NumbersLibrary
{
    public interface INumberPrinter
    {
        void PrintNumbers(int lower = 1, int upper = 100);
        void Register(int divisor, string outputText);
    }
}