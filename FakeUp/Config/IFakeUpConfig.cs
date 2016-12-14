using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FakeUp.Fluent;

namespace FakeUp.Config
{
    public interface IFakeUpConfig<TFakeObject>
    {
        // TODO: add ability to register type to fill all it's base classes
        /* so following call should be possible:
         *
         * .FillAllAncestersOf<Foo>().With([Foo expected])
         */

        IWith<TFakeObject, TMember> FillAll<TMember>();

        IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath);

        IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(Expression<Func<TMember, TMetaMember>> memberPath);

        ICollectionWith<TFakeObject> FillElementsOf<TCollection>()
            where TCollection : IEnumerable;

        ICollectionWith<TFakeObject> FillElementsOf<TCollection>(Expression<Func<TFakeObject, TCollection>> memberPath)
            where TCollection : IEnumerable;

        IFakeUpConfig<TFakeObject> WithCollectionsSize(int defaultSize);

        IFakeUpConfig<TFakeObject> WithCollectionsSize<TCollection>(
            Expression<Func<TFakeObject, TCollection>> collectionPath, int size
        ) where TCollection : IEnumerable;

        IFakeUpConfig<TFakeObject> WithCollectionsSize<TRelative, TCollection>(
            Expression<Func<TRelative, TCollection>> collectionPath, int size
        ) where TCollection : IEnumerable;

        IFakeUpConfig<TFakeObject> WithCollectionsSize<TCollection>(int size)
            where TCollection : IEnumerable;

        // TODO: add ability to set default configuration
        /*  so following calls should be possible:
         *
         *  FakeUpConfig.SetDefault(conf => [whatever]);
         *  FakeUpConfig.ClearDefault()
         */

        // TODO: add option to use strict mode
        /* in this mode, activator evaluator should not be used and exception shall be thrown
         *  so following call should be possible:
         *
         *  .InStrictMode()
         */

        IFakeUpConfig<TFakeObject> WithConfiguration(params Action<IFakeUpConfig<TFakeObject>>[] configs);

        IFakeUpConfig<TFakeObject> WithConfigurations(IEnumerable<Action<IFakeUpConfig<TFakeObject>>> configs);
    }
}