using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Reflection;

namespace Plumbing.DataAccess
{
    public class EntityFrameworkDataContext : IDataContext
    {
        readonly ObjectContext context;
        readonly IDictionary<Type, PropertyInfo> queries;
        readonly IDictionary<Type, MethodInfo> inserts;
        readonly IDictionary<Type, object> queryCache;

        public EntityFrameworkDataContext(ObjectContext context)
        {
            this.context = context;
            queries = new Dictionary<Type, PropertyInfo>();
            inserts = new Dictionary<Type, MethodInfo>();
            queryCache = new Dictionary<Type, object>();

            var queryType = typeof (ObjectQuery<>);
            var contextType = context.GetType();
            var queryProperties = contextType.GetProperties().Where(p => IsGenericType(p.PropertyType, queryType));
            foreach (var queryProperty in queryProperties)
            {
                var genericType = queryProperty.PropertyType.GetGenericArguments().First();
                queries[genericType] = queryProperty;

                var addMethod = contextType.GetMethods().FirstOrDefault(m => IsAddToMethod(m, genericType));
                if (addMethod != null)
                {
                    inserts[genericType] = addMethod;
                }
            }
        }

        static bool IsGenericType(Type type, Type genericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        static bool IsAddToMethod(MethodInfo method, Type genericType)
        {
            if (!method.Name.StartsWith("AddTo")) return false;

            var paramaters = method.GetParameters();
            if (paramaters.Count() != 1) return false;

            return paramaters[0].ParameterType == genericType;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var type = typeof (T);
            if (queryCache.ContainsKey(type))
            {
                return (ObjectQuery<T>)queryCache[type];
            }

            if (!queries.ContainsKey(type))
            {
                throw new NotImplementedException();
            }

            var queryProperty = queries[type];
            var query = queryProperty.GetGetMethod().Invoke(context, new object[0]);
            queryCache[type] = query;
            return (ObjectQuery<T>)query;
        }

        public void Insert<T>(T entity) where T : class
        {
            var type = typeof(T);
            if (!inserts.ContainsKey(type))
            {
                throw new NotImplementedException();
            }

            var insertMethod = inserts[type];
            insertMethod.Invoke(context, new object[] { entity });
        }

        public void Delete<T>(T entity) where T : class
        {
            context.DeleteObject(entity);
        }

        public void SubmitChanges()
        {
            context.SaveChanges();
        }

        public int ExecuteCommand(string command, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TResult> ExecuteQuery<TResult>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}