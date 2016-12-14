﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FakeUp.Extensions;
using FakeUp.Fluent;
using FakeUp.Fluent.Implementation;
using FakeUp.RelativePathing;

namespace FakeUp.Config
{
    internal class FakeUpConfig<TFakeObject> : IFakeUpConfig<TFakeObject>, IInternalFakeUpConfig
    {
        public const int DefaultCollectionElementCount = 1;

        protected Func<int> ElementsInCollectionsSourceFunc;

        public FakeUpConfig()
        {
            this.AbsoluteElementsFillers = new Dictionary<string, Func<int, object>>();
            this.RelativeElementsFillers = new List<FillerRelativeMemberInfo>();
            this.TypeElementsFillers = new Dictionary<Type, Func<int, object>>();
            this.AbsolutePathFillers = new Dictionary<string, Func<object>>();
            this.RelativeTypeFillers = new List<FillerRelativeMemberInfo>();
            this.TypeFillers = new Dictionary<Type, Func<object>>();
            this.AbsolutePathCollectionSizes = new Dictionary<string, int>();
            this.TypeCollectionSizes = new Dictionary<Type, int>();
            this.RelativeCollectionSizes = new List<CollectionSizeRelativeMemberInfo>();
            this.DefaultCollectionsSize = DefaultCollectionElementCount;
        }

        public int DefaultCollectionsSize { get; set; }

        #region Member fillers

        public Dictionary<Type, Func<object>> TypeFillers { get; set; }

        public List<FillerRelativeMemberInfo> RelativeTypeFillers { get; set; }

        public Dictionary<string, Func<object>> AbsolutePathFillers { get; set; }

        #endregion Member fillers

        #region Element fillers

        public Dictionary<Type, Func<int, object>> TypeElementsFillers { get; set; }

        public List<FillerRelativeMemberInfo> RelativeElementsFillers { get; set; }

        public Dictionary<string, Func<int, object>> AbsoluteElementsFillers { get; set; }

        #endregion Element fillers

        #region Collection sizes

        public Dictionary<string, int> AbsolutePathCollectionSizes { get; set; }

        public Dictionary<Type, int> TypeCollectionSizes { get; set; }

        public List<CollectionSizeRelativeMemberInfo> RelativeCollectionSizes { get; set; }

        #endregion Collection sizes

        public ICollectionWith<TFakeObject> FillElementsOf<TCollection>(
            Expression<Func<TFakeObject, TCollection>> memberPath)
            where TCollection : IEnumerable
        {
            return new WithAbsoluteCollectionPath<TFakeObject, TCollection>(memberPath, this);
        }

        public IFakeUpConfig<TFakeObject> WithCollectionsSize(int elementsCount = DefaultCollectionElementCount)
        {
            this.ElementsInCollectionsSourceFunc = () => elementsCount;
            return this;
        }

        public IFakeUpConfig<TFakeObject> WithCollectionsSize<TCollection>(Expression<Func<TFakeObject, TCollection>> collectionPath, int size)
            where TCollection : IEnumerable
        {
            var callPath = collectionPath.ToCallPath();
            this.AbsolutePathCollectionSizes[callPath] = size;

            return this;
        }

        public IFakeUpConfig<TFakeObject> WithCollectionsSize<TRelative, TCollection>(Expression<Func<TRelative, TCollection>> collectionPath, int size) where TCollection : IEnumerable
        {
            var relativeMemberInfo = new CollectionSizeRelativeMemberInfo(size, typeof(TRelative), typeof(TCollection), collectionPath.ToCallChain());
            this.RelativeCollectionSizes.Add(relativeMemberInfo);

            return this;
        }

        public IFakeUpConfig<TFakeObject> WithCollectionsSize<TCollection>(int size)
            where TCollection : IEnumerable
        {
            this.TypeCollectionSizes[typeof(TCollection)] = size;

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

        public IFakeUpConfig<TFakeObject> WithConfiguration(params Action<IFakeUpConfig<TFakeObject>>[] configs)
        {
            return WithConfigurations(configs);
        }

        public IFakeUpConfig<TFakeObject> WithConfigurations(IEnumerable<Action<IFakeUpConfig<TFakeObject>>> configs)
        {
            foreach (var config in configs)
            {
                config(this);
            }
            return this;
        }
    }
}