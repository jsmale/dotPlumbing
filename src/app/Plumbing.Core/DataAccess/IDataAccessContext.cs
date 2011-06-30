using System;
using System.Collections.Generic;

namespace Plumbing.DataAccess
{
	public interface IDataAccessContext
	{
        T GetCurrentDataContext<T>() where T : class, IDataContext;
        void SetCurrentDataContext<T>(T dataContext) where T : class, IDataContext;
	    void SetCurrentDataContext(IDataContext dataContext, Type dataContextType);
        IEnumerable<IDataContext> GetAllCurrentDataContexts();
    }
}