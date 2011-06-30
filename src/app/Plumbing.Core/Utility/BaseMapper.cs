using System;
using System.Linq.Expressions;

namespace Plumbing.Utility
{
    public abstract class BaseMapper<TFrom, TTo> : IMapper<TFrom, TTo>
    {
        Func<TFrom, TTo> mapper;
        Func<TFrom, TTo> Mapper
        {
            get { return mapper ?? (mapper = MappingExpression.Compile()); }
        }

        public TTo Map(TFrom from)
        {
            return Mapper(from);
        }

        public abstract Expression<Func<TFrom, TTo>> MappingExpression { get; }
    }
}