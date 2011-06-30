using System;
using System.Collections.Generic;
using System.Linq;
using Plumbing.DataAccess;
using Raven.Client;

namespace Plumbing.RavenDb
{
    public class RavenDbDataContext : IDataContext
    {
        readonly IDocumentSession session;

        public RavenDbDataContext(IDocumentSession session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            session.Dispose();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return session.Query<T>();
        }

        public void Insert<T>(T entity) where T : class
        {
            session.Store(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            session.Delete(entity);
        }

        public void SubmitChanges()
        {
            session.SaveChanges();
        }

        public int ExecuteCommand(string command, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}