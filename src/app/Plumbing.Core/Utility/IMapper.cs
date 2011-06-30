using System;
using System.Linq.Expressions;

namespace Plumbing.Utility
{
    public interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom from);
        Expression<Func<TFrom, TTo>> MappingExpression { get; }
    }
}