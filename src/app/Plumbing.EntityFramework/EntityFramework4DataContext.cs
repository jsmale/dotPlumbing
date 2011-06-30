using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Plumbing.DataAccess;

namespace Plumbing.EntityFramework
{
    public class EntityFramework4DataContext : IDataContext
    {
        readonly ObjectContext context;
        readonly IDictionary<Type, object> objectSets;
        
        public EntityFramework4DataContext(ObjectContext context)
        {
            this.context = context;
            objectSets = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return GetObjectSet<T>();
        }

        ObjectSet<T> GetObjectSet<T>() where T : class
        {
            var type = typeof (T);
            if (objectSets.ContainsKey(type))
            {
                return (ObjectSet<T>) objectSets[type];
            }

            var set = context.CreateObjectSet<T>();
            objectSets[type] = set;
            return set;
        }

        public void Insert<T>(T entity) where T : class
        {
            GetObjectSet<T>().AddObject(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            GetObjectSet<T>().DeleteObject(entity);
        }

        public void SubmitChanges()
        {
            context.SaveChanges();
        }

        public int ExecuteCommand(string command, params object[] parameters)
        {
            return context.ExecuteStoreCommand(command, parameters);
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters)
        {
            return context.ExecuteStoreQuery<TResult>(query, parameters);
        }
    }
}
