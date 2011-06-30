using System.Data.Entity;
using Cqrs.Sample.ViewModels.Infrastructure;
using StructureMap.Configuration.DSL;
using System.Linq;

namespace Cqrs.Sample.QueryService.Infrastructure
{
    public class QueryServiceInitializer : ViewModelInitializer
    {
        public QueryServiceInitializer(string homeDirectory, params Registry[] additionalRegistries)
            : base(homeDirectory, additionalRegistries.Concat(new []{new QueryServiceRegistry()}).ToArray())
        {
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

            var initializer = new RecreateViewModelsFromEvents();
            Database.SetInitializer(initializer);
            (new ViewModelContext()).Users.FirstOrDefault();            
            if (initializer.NeedsData)
            {
                initializer.UpdateModelsFromEvents();
            }
        }
    }
}