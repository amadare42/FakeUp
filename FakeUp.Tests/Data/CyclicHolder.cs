namespace Playground.ObjectFaker.Tests
{
    public class CyclicHolder<T>
    {
        public CyclicHolder<T> InnerHolder { get; set; }
        public T Value { get; set; }
    }
}