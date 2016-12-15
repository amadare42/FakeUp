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

        // TODO: implement dealing with classes without parameterless c-tor


        // TODO: fill types assignable from
        /*
         * like FillAll (by type)
         *   .FillAssignableFrom<TMember>()
         *
         * like Fill<TMember> (by abs. path)
         *   .FillAssignableFrom<TMember>(fakeObject => fakeObject.Member)
         *
         * like Fill<TMember, TMetaMember> (by relative type)
         *   .FillAssignableFrom<TMember, TMetaMember>(member => member.MetaValue)
         *
         * /

        // TODO: fill by name matching
        /*
         * .Fill<string>().NamedLike("*FirstName").OfType<string>().With(() => Faker.Names.Generate());
         * .FillNamedLike("*FirstName").ThatAssignableFrom<string>().With(() => Faker.Names.Generate());
         * .FillNamedLike("*Dummy*Object").With(() => new Dummy());
         *
         */

        // TODO: saved config should have dedicated class
        /*
         * It should contain name and it should be possible to get filling rules information (including config tree).
         */

        // TODO: [after saved config dedicated class] add ability to add name for generated fillers
        /*
         * var nameFiller = FakeUp.Config.Create<User>(c => c.NameConfig("NameFiller").Fill(u => u.Name).With("name"));
         * nameFiller.Name == "NameFiller"
         */

        // TODO: [after saved config dedicated class] add ability to dump info for
        /*
         * var nameFiller = FakeUp.Config.Create<User>("NameFiller", c => c.Fill(u => u.Name).With("name"));
         * nameFiller.Name == "NameFiller"
         */

        // TODO: use config to fill member
        /*
         *
         * var smithNameFiller = FakeUp.Config.Create<User>(c => c.Fill(user => user.Name).With("Agent Smith"));
         * var personNameFiller = FakeUp.Config.Create<User>(c => c.Fill(user => user.Name).With(() => Faker.Names.Create());
         *
         * ...
         * .FillElementsOf(foo => foo.UserCollection).With(userCreator)
         * ...
         * FakeUp.CreateBy(userCreator)
         * ...
         *
         */

        // TODO: add ability to set random collection size
        /* so following call should be possible:
         *
         * .WithRandomCollectionsSize()
         * .WithRandomCollectionsSize(100)
         * .WithRandomCollectionsSize(10, 100)
         */

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