using System.Linq;
using Plumbing.Domain;

namespace Plumbing.DataAccess
{
	public class Repository : IRepository 
	{
		readonly IDataContext dataContext;

		public Repository(IDataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public IQueryable<T> Query<T>() where T : class
		{
			return dataContext.Query<T>();
		}

	    public T Get<T,TKey>(TKey id) where T : class, IEntity<TKey>
	    {
	        return Query<T>().FirstOrDefault(entity => Equals(entity.Id, id));
	    }

	    public void Insert<T>(T entity) where T : class
	    {
	        dataContext.Insert(entity);
	    }

	    public void Delete<T>(T entity) where T : class
	    {
	        dataContext.Delete(entity);
	    }

	    public void SubmitChanges()
	    {
	        dataContext.SubmitChanges();
	    }
	}
}