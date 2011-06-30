using Plumbing.Container;
using Plumbing.Extensions;

namespace Plumbing.DataAccess
{
    public class DisposeCurrentDataContext : IDisposeCurrentDataContext 
    {
        readonly IContainer container;

        public DisposeCurrentDataContext(IContainer container)
        {
            this.container = container;
        }

        public void DisposeTheCurrentDataContext()
        {
            var dataAccessContext = container.GetInstance<IDataAccessContext>();
            var dataContexts = dataAccessContext.GetAllCurrentDataContexts();
            dataContexts.Each(dataContext =>
            {
                dataContext.SubmitChanges();
                dataContext.Dispose();
            });
        }
    }
}