using System;
using System.Collections.Generic;
using System.Linq;

namespace Plumbing.DataAccess
{
	public interface IDataContext : IDisposable
	{
		IQueryable<T> Query<T>() where T : class;
        void Insert<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
		void SubmitChanges();
	    int ExecuteCommand(string command, params object[] parameters);
	    IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters);
	}
}