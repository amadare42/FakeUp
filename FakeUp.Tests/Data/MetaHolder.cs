namespace FakeUp.Tests.Data
{
    public class MetaHolder<T>
    {
        public ValuesHolder<T> Holder { get; set; }
        public T Value { get; set; }
    }
}