using System;
using System.Collections.Generic;
using System.Reflection;

namespace Playground.ObjectFaker
{
    static class PopulatorManager
    {
        public static void Populate<T>(T instanse, FakerOptions<T> options)
        {
            var context = new ObjectCreationContext<T>
            {
                TypedRootObject = instanse,
                TypedOptions = options
            };

            PopulateInstanse(instanse, options.PopulateRootExpressions);
        }

        private static void PopulateInstanse<T>(T instanse, Dictionary<string, IValuePopulator<T>> populateRootExpressions)
        {
            foreach (var pair in populateRootExpressions)
            {
                var path = pair.Key;
                var evaluator = pair.Value;
                var setter = GetDeepPropertyInfo(instanse, path);

                var value = evaluator.Evaluate(instanse);
                setter.SetValue(value);
            }
        }

        public static PropertySetter GetDeepPropertyInfo(object instanse, string path)
        {
            var pp = path.Split('.');
            PropertySetter result = null;
            Type t = instanse.GetType();
            foreach (var prop in pp)
            {
                var propInfo = t.GetProperty(prop);
                if (propInfo != null)
                {
                    result = new PropertySetter()
                    {
                        PropertyInfo = propInfo,
                        Instanse = instanse
                    };

                    instanse = propInfo.GetValue(instanse, null);
                }
                else throw new ArgumentException("Properties path is not correct");
            }
            return result;
        }

        public class PropertySetter
        {
            public PropertyInfo PropertyInfo { get; set; }
            public object Instanse { get; set; }

            public void SetValue(object value)
            {
                PropertyInfo.SetValue(Instanse, value);
            }
        }
    }
}