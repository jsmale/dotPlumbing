using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace Cqrs.Sample.ViewModels.Infrastructure
{
    public class ViewModelContext : DbContext
    {
        public ViewModelContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public ObjectContext InnerContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
    }
}