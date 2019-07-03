using System.Collections.Generic;

namespace FakeUp.Tests.Data
{
    public class ValuesHolder<T>
    {
        public T Value1 { get; set; }
        public T Value2 { get; set; }
        public T Value3 { get; set; }
    }

    public class IntValueHolder : ValuesHolder<int>
    {
        
    }

    public class ValuesHolder<T1, T2>
    {
        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
    }
    
    public class IntListValueHolder : ValuesHolder<int[], List<int>>
    {
    }
}