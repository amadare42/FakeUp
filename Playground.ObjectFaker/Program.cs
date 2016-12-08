using System.Collections.Generic;

namespace Playground.ObjectFaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }

    public class Data
    {
        public string MyString { get; set; }
        public Data2 InnerData2 { get; set; }
        public Data2 EmptyInnerData2 { get; set; }
        public List<Data2> Data2s { get; set; }
        public Data2[] Data2sArr { get; set; }
    }

    public class Data2
    {
        public int MyInt { get; set; }
        public Data3 InnerData3 { get; set; }
        public string MyString { get; set; }
    }

    public class Data3
    {
        public int MyInt { get; set; }
        public string MyString { get; set; }
    }
}