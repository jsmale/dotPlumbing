using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Plumbing.DataAccess
{
    public class LinqDataContext : IDataContext
    {
        readonly DataContext dataContext;

        public LinqDataContext(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Dispose()
        {
            dataContext.Dispose();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Table<T>();
        }

        Table<T> Table<T>() where T : class
        {
            return dataContext.GetTable<T>();
        }

        public void Insert<T>(T entity) where T : class
        {
            Table<T>().InsertOnSubmit(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Table<T>().InsertOnSubmit(entity);
        }

        public void SubmitChanges()
        {
            dataContext.SubmitChanges();
        }

        public int ExecuteCommand(string command, params object[] parameters)
        {
            return dataContext.ExecuteCommand(command, parameters);
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters)
        {
            return dataContext.ExecuteQuery<TResult>(query, parameters);
        }
    }
}