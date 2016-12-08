using System;
using System.Collections;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    public interface IFakeUpOptions<TFakeObject>
    {
        IWith<TFakeObject, TMember> FillAll<TMember>();

        IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath);

        IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(Expression<Func<TMember, TMetaMember>> memberPath);

        ICollectionWith<TFakeObject> FillElementsOf<TCollection>()
            where TCollection : IEnumerable;

        ICollectionWith<TFakeObject> FillElementsOf<TCollection>(Expression<Func<TFakeObject, TCollection>> memberPath)
            where TCollection : IEnumerable;

        IFakeUpOptions<TFakeObject> WithCollectionsSize(int elementsCount = FakeUp.DefaultCollectionElementCount);
    }
}