using System;
using System.Collections.Generic;
using Cqrs.Sample.Common.Infrastructure;
using Plumbing.DataAccess;
using Plumbing.EntityFramework;
using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.ViewModels.Infrastructure
{
    public class ViewModelInitializer : CommonInitializer
    {
        public ViewModelInitializer(string homeDirectory, params Registry[] additionalRegistries)
            : base(homeDirectory, additionalRegistries)
        {
        }

        protected override IDictionary<Type, Func<IDataContext>> CreateDataContextFuncs()
        {
            return new Dictionary<Type, Func<IDataContext>>
            {
                {typeof(IDataContext),() => new EntityFramework4DataContext((new ViewModelContext()).InnerContext)}
            };
        }
    }
}