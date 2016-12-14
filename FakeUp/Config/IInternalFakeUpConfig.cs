using System;
using System.Collections.Generic;
using FakeUp.RelativePathing;

namespace FakeUp.Config
{
    internal interface IInternalFakeUpConfig
    {
        int DefaultCollectionsSize { get; }

        Dictionary<Type, Func<object>> TypeFillers { get; }

        List<FillerRelativeMemberInfo> RelativeTypeFillers { get; }

        Dictionary<string, Func<object>> AbsolutePathFillers { get; }

        Dictionary<Type, Func<int, object>> TypeElementsFillers { get; }

        List<FillerRelativeMemberInfo> RelativeElementsFillers { get; }

        Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; }

        Dictionary<string, int> AbsolutePathCollectionSizes { get; set; }

        Dictionary<Type, int> TypeCollectionSizes { get; set; }

        List<CollectionSizeRelativeMemberInfo> RelativeCollectionSizes { get; set; }
    }
}