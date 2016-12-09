using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FakeUp.Fluent;

namespace FakeUp
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

        // TODO: extend ability to specify collection size
        /*  so following calls should be possible:
         *
         *  .WithCollectionSize(3)
         *  .WithCollectionSize(foo => foo.BarCollection, 3)
         *  .WithCollectionSize<Bar[]>(3)
         *  .WithCollectionSize(baz => baz.foo.BarCollection, 3)
        */

        // TODO: move DefaultCollectionElementCount constant to other place
        IFakeUpConfig<TFakeObject> WithCollectionsSize(int elementsCount = FakeUp.DefaultCollectionElementCount);

        // TODO: add ability to use previously created config as base
        /*  so following calls should be possible:
         *
         *  var fooFillingConfig = FakeUpConfig.Create(conf => conf.Fill<Foo>().With(new Foo()));
         *  var quxFillingConfig = FakeUpConfig.Create(conf => conf.Fill<Qux>().With(new Qux()));
         *
         *  FakeUp.NewObject<Bar>(conf => conf
         *      .WithConfigurations(fooFillingConfig, quxFillingConfig)
         *      .Fill<Baz>().With(null)
         * )
         */

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

        IFakeUpConfig<TFakeObject> WithConfigurations(params Action<IFakeUpConfig<TFakeObject>>[] configs);

        IFakeUpConfig<TFakeObject> WithConfigurations(IEnumerable<Action<IFakeUpConfig<TFakeObject>>> configs);
    }
}