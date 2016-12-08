using System;

namespace Playground.ObjectFaker
{
    internal class RelativeMemberInfo
    {
        private readonly Func<object> factoryFunc;
        public Type RootType { get; set; }
        public string RelativePath { get; set; }

        public RelativeMemberInfo(Func<object> factoryFunc)
        {
            this.factoryFunc = factoryFunc;
        }

        public object Evaluate()
        {
            return this.factoryFunc();
        }

        public AbsoluteMemberInfo ToAbsoluteMemberInfo(string invokationPath)
        {
            var absolutePath = $"{invokationPath}.{this.RelativePath}";
            return new AbsoluteMemberInfo(this.factoryFunc, absolutePath, this.RootType);
        }

        public string GetAbsolutePath(string invokationPath)
        {
            return $"{invokationPath}.{this.RelativePath}";
        }
    }
}