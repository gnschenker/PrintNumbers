using System.Collections.Generic;
using NumbersLibrary;

namespace PrintNumbers
{
    public class OutputMock : IOutput
    {
        public OutputMock()
        {
            List= new List<string>();
        }

        public List<string> List { get; set; }
        public void Write(string number)
        {
            List.Add(number);
        }
    }
}