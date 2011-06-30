using System;
using System.Collections.Generic;
using System.Linq;

namespace Plumbing.DataAccess
{
	public class DataAccessContext : IDataAccessContext
	{
        readonly IDictionary<Type, IDataContext> dataContexts;

        public DataAccessContext()
	    {
	        dataContexts = new Dictionary<Type, IDataContext>();
	    }

	    public T GetCurrentDataContext<T>() where T : class, IDataContext
	    {
	        var type = typeof (T);
            if (!dataContexts.ContainsKey(type)) return null;

	        return (T)dataContexts[type];
	    }

	    public void SetCurrentDataContext<T>(T dataContext) where T : class, IDataContext
	    {
            SetCurrentDataContext(dataContext, typeof(T));
	    }

	    public void SetCurrentDataContext(IDataContext dataContext, Type dataContextType)
        {
            dataContexts[dataContextType] = dataContext;
	    }

	    public IEnumerable<IDataContext> GetAllCurrentDataContexts()
	    {
	        return dataContexts.Select(x => x.Value);
	    }
	}
}