using System.Linq;
using Plumbing.Domain;

namespace Plumbing.DataAccess
{
	public interface IRepository
	{
		IQueryable<T> Query<T>() where T : class;
        T Get<T, TKey>(TKey id) where T : class, IEntity<TKey>;
        void Insert<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void SubmitChanges();
	}
}