using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    public class FakeUpOptions
    {
        protected Func<int> ElementsInCollectionsSourceFunc;

        public FakeUpOptions()
        {
            this.CustomTypeFactories = new Dictionary<Type, Func<object>>();
            this.TypePopulators = new Dictionary<Type, Action<object>>();
            this.ElementsInCollectionsSourceFunc = () => FakeUp.DefaultCollectionElementCount;
        }

        public Dictionary<Type, Action<object>> TypePopulators { get; }

        public Dictionary<Type, Func<object>> CustomTypeFactories { get; }

        public int ElementsInCollections => this.ElementsInCollectionsSourceFunc();

        public string DefaultStringValue { get; set; }
    }

    public class FakeUpOptions<TFakeObject> : FakeUpOptions, IFakeUpOptions<TFakeObject>
    {
        public FakeUpOptions<TFakeObject> RandomCollectionElementsCount(int min = 1, int max = 15)
        {
            this.ElementsInCollectionsSourceFunc = () => new Random().Next(min, max);
            return this;
        }

        public ICollectionWith<TFakeObject> FillElementsOf<TCollection>(Expression<Func<TFakeObject, TCollection>> memberPath) 
            where TCollection : IEnumerable
        {
            return new WithAbsoluteCollectionPath<TFakeObject,TCollection>(memberPath, this);
        }

        public IFakeUpOptions<TFakeObject> WithCollectionsSize(int elementsCount = FakeUp.DefaultCollectionElementCount)
        {
            this.ElementsInCollectionsSourceFunc = () => elementsCount;
            return this;
        }

        //// ADDED--------------

        internal Dictionary<Type, Func<object>> TypeFillers { get; set; } = new Dictionary<Type, Func<object>>();

        internal List<RelativeMemberInfo> RelativeTypeFillers { get; set; } = new List<RelativeMemberInfo>();

        // TODO: change to RelativeMemberInfo mechanism
        internal Dictionary<string, Func<object>> RootMemberFillers { get; set; } = new Dictionary<string, Func<object>>();

        internal Dictionary<Type, Func<int, object>> ElementsTypeFillers { get; set; } = new Dictionary<Type, Func<int, object>>();

        internal List<RelativeMemberInfo> RelativeElementsFillers { get; set; } = new List<RelativeMemberInfo>();

        internal Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; set; } = new Dictionary<string, Func<int, object>>();

        public IWith<TFakeObject, TMember> FillAll<TMember>()
        {
            return new WithType<TFakeObject, TMember>(this);
        }

        public IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath)
        {
            return new WithAbsolutePath<TFakeObject, TMember>(this, memberPath);
        }

        public IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(Expression<Func<TMember, TMetaMember>> memberPath)
        {
            return new WithRelativePath<TFakeObject, TMember, TMetaMember>(this, memberPath);
        }

        public ICollectionWith<TFakeObject> FillElementsOf<TCollection>() where TCollection : IEnumerable
        {
            return new WithCollectionType<TFakeObject, TCollection>(this);
        }
    }
}