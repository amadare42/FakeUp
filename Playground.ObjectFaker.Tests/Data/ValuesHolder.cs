namespace Playground.ObjectFaker.Tests
{
    public class ValuesHolder<T>
    {
        public T Value1 { get; set; }
        public T Value2 { get; set; }
    }

    public class ValuesHolder<T1, T2>
    {
        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
    }

    public class MetaIntHolder
    {
        public IntHolder Holder { get; set; }
    }

    public class IntHolder
    {
        public int IntValue1 { get; set; }
        public int IntValue2 { get; set; }
    }
}


/*
 *  // Fill specific types during generation time
    //     with built-in rule
    opt.FillAll<int>().WithRandomValue(10, 100);
    opt.FillAll<string>().WithLorem();
    // OR  with constant
    opt.FillAll<int>().With(42);
    // OR  with function
    opt.FillAll<int>().With(() => ++d);

    // FillAll will be overrided by Fill call for matching props
    opt.Fill(foo => foo.Bar).With(3);

    // Fill can fill inner members
    // OR by relative addressing
    opt.Fill<Baz>(baz => baz.Qux).With("Qux");

    // For array members FillMemberOf should be called
    opt.FillMembersOf<Bar[]>(bar => bar.Qux).With("Qux in array");
    opt.FillMembersOf(foo => foo.Bars).With((int index) => new Bar(index));
*/