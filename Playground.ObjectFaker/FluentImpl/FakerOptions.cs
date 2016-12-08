using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    public class FakerOptions
    {
        public const int DefaultCollectionElementCount = 1;

        protected Func<int> ElementsInCollectionsSourceFunc;

        public FakerOptions()
        {
            this.CustomTypeFactories = new Dictionary<Type, Func<object>>();
            this.TypePopulators = new Dictionary<Type, Action<object>>();
            this.ElementsInCollectionsSourceFunc = () => DefaultCollectionElementCount;
        }

        public Dictionary<Type, Action<object>> TypePopulators { get; }

        public Dictionary<Type, Func<object>> CustomTypeFactories { get; }

        public List<ExpressionHolder> RelativeExpressions { get; } = new List<ExpressionHolder>();

        public int ElementsInCollections => this.ElementsInCollectionsSourceFunc();

        public string DefaultStringValue { get; set; }
    }

    public class FakerOptions<TFakeObject> : FakerOptions, IOptions<TFakeObject>
    {
        public Dictionary<string, IValuePopulator<TFakeObject>> PopulateRootExpressions { get; } = new Dictionary<string, IValuePopulator<TFakeObject>>();

        public FakerOptions<TFakeObject> RandomCollectionElementsCount(int min = 1, int max = 15)
        {
            this.ElementsInCollectionsSourceFunc = () => new Random().Next(min, max);
            return this;
        }

        public FakerOptions<TFakeObject> CollectionsElementsCount(int elementsCount = DefaultCollectionElementCount)
        {
            this.ElementsInCollectionsSourceFunc = () => elementsCount;
            return this;
        }

        public FakerOptions<TFakeObject> TypeFactory<T>(Func<T> factoryFunc)
        {
            this.CustomTypeFactories.Add(typeof(T), () => factoryFunc());
            return this;
        }

        public FakerOptions<TFakeObject> PopulateRelative<TObject, TMember>(Expression<Func<TObject, TMember>> expr)
        {
            var callPath = expr.ToCallPath();
            var holder = new ExpressionHolder<TObject, TMember>(expr, callPath);
            RelativeExpressions.Add(holder);
            return this;
        }

        public IFluentPopulator<TFakeObject, TMember> Populate<TMember>(Expression<Func<TFakeObject, TMember>> expr)
        {
            return new RootFluentPopulator<TFakeObject, TMember>(this, expr);
        }

        //todo: default string value

        //// ADDED--------------

        public Dictionary<Type, Func<object>> TypeFillers { get; set; } = new Dictionary<Type, Func<object>>();

        internal Dictionary<Type, RelativeMemberInfo> RelativeTypeFillers { get; set; } = new Dictionary<Type, RelativeMemberInfo>();

        public Dictionary<string, Func<object>> RootMemberFillers { get; set; } = new Dictionary<string, Func<object>>();

        public IWith<TFakeObject, TMember> FillAll<TMember>()
        {
            return new WithTypeFiller<TFakeObject, TMember>(this);
        }

        public IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath)
        {
            return new WithMemberFiller<TFakeObject, TMember>(this, memberPath);
        }

        public IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(Expression<Func<TMember, TMetaMember>> memberPath)
        {
            return new WithRelativeTypeFiller<TFakeObject, TMember, TMetaMember>(this, memberPath);
        }
    }
}