using System;
using System.Collections.Generic;
using FakeUpLib.RelativePathing;
using FakeUpLib.ValueEvaluation;

namespace FakeUpLib.Config
{
    public interface IInternalFakeUpConfig
    {
        int DefaultCollectionsSize { get; }

        Dictionary<Type, Func<IObjectCreationContext, object>> TypeFillers { get; }

        List<FillerRelativeMemberInfo> RelativeTypeFillers { get; }

        Dictionary<string, Func<IObjectCreationContext, object>> AbsolutePathFillers { get; }

        Dictionary<Type, Func<int, object>> TypeElementsFillers { get; }

        List<FillerRelativeMemberInfo> RelativeElementsFillers { get; }

        Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; }

        Dictionary<string, int> AbsolutePathCollectionSizes { get; set; }

        Dictionary<Type, int> TypeCollectionSizes { get; set; }

        List<CollectionSizeRelativeMemberInfo> RelativeCollectionSizes { get; set; }

        List<IValueEvaluator> ValueEvaluators { get; }
    }
}