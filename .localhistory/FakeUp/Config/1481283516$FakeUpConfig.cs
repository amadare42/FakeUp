using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FakeUp.Fluent;
using FakeUp.FluentImpl;

namespace FakeUp
{
    internal class FakeUpConfig<TFakeObject> : IFakeUpConfig<TFakeObject>, IInternalFakeUpConfig
    {
        protected Func<int> ElementsInCollectionsSourceFunc;

        public FakeUpConfig()
        {
            this.AbsoluteElementsFillers = new Dictionary<string, Func<int, object>>();
            this.RelativeElementsFillers = new List<RelativeMemberInfo>();
            this.TypeElementsFillers = new Dictionary<Type, Func<int, object>>();
            this.AbsolutePathFillers = new Dictionary<string, Func<object>>();
            this.RelativeTypeFillers = new List<RelativeMemberInfo>();
            this.TypeFillers = new Dictionary<Type, Func<object>>();
            this.ElementsInCollectionsSourceFunc = () => FakeUp.DefaultCollectionElementCount;
        }

        public int ElementsInCollections => this.ElementsInCollectionsSourceFunc();

        public Dictionary<Type, Func<object>> TypeFillers { get; set; }

        public List<RelativeMemberInfo> RelativeTypeFillers { get; set; }

        public Dictionary<string, Func<object>> AbsolutePathFillers { get; set; }

        public Dictionary<Type, Func<int, object>> TypeElementsFillers { get; set; }

        public List<RelativeMemberInfo> RelativeElementsFillers { get; set; }

        public Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; set; }

        public ICollectionWith<TFakeObject> FillElementsOf<TCollection>(
            Expression<Func<TFakeObject, TCollection>> memberPath)
            where TCollection : IEnumerable
        {
            return new WithAbsoluteCollectionPath<TFakeObject, TCollection>(memberPath, this);
        }

        public IFakeUpConfig<TFakeObject> WithCollectionsSize(int elementsCount = FakeUp.DefaultCollectionElementCount)
        {
            this.ElementsInCollectionsSourceFunc = () => elementsCount;
            return this;
        }

        public IWith<TFakeObject, TMember> FillAll<TMember>()
        {
            return new WithType<TFakeObject, TMember>(this);
        }

        public IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath)
        {
            return new WithAbsolutePath<TFakeObject, TMember>(this, memberPath);
        }

        public IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(
            Expression<Func<TMember, TMetaMember>> memberPath)
        {
            return new WithRelativePath<TFakeObject, TMember, TMetaMember>(this, memberPath);
        }

        public ICollectionWith<TFakeObject> FillElementsOf<TCollection>()
            where TCollection : IEnumerable
        {
            return new WithCollectionType<TFakeObject, TCollection>(this);
        }

        public IFakeUpConfig<TFakeObject> WithConfigurations<T>(params Action<IFakeUpConfig<T>>[] configs)
        {
            throw new NotImplementedException();
        }

        public IFakeUpConfig<TFakeObject> WithConfigurations<T>(IEnumerable<Action<IFakeUpConfig<T>>> configs)
        {
            throw new NotImplementedException();
        }
    }
}