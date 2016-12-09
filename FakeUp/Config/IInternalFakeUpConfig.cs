using System;
using System.Collections.Generic;

namespace FakeUp
{
    internal interface IInternalFakeUpConfig
    {
        int ElementsInCollections { get; }

        Dictionary<Type, Func<object>> TypeFillers { get; }

        List<RelativeMemberInfo> RelativeTypeFillers { get; }

        Dictionary<string, Func<object>> AbsolutePathFillers { get; }

        Dictionary<Type, Func<int, object>> TypeElementsFillers { get; }

        List<RelativeMemberInfo> RelativeElementsFillers { get; }

        Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; }
    }
}