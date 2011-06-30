using System.Web.Mvc;
using Plumbing.DataAccess;

namespace Plumbing.Web.Mvc.Filters
{
    public class DataContextFilter : IActionFilter
    {
        readonly IDisposeCurrentDataContext disposeCurrentDataContext;

        public DataContextFilter(IDisposeCurrentDataContext disposeCurrentDataContext)
        {
            this.disposeCurrentDataContext = disposeCurrentDataContext;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            disposeCurrentDataContext.DisposeTheCurrentDataContext();
        }
    }
}