using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    public interface IOptions<TFakeObject>
    {
        IWith<TFakeObject, TMember> FillAll<TMember>();

        IWith<TFakeObject, TMember> Fill<TMember>(Expression<Func<TFakeObject, TMember>> memberPath);

        IWith<TFakeObject, TMetaMember> Fill<TMember, TMetaMember>(Expression<Func<TMember, TMetaMember>> memberPath);
    }
}