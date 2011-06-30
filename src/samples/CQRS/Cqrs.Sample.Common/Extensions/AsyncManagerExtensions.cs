using System.Web.Mvc.Async;

namespace Cqrs.Sample.Common.Extensions
{
    public static class AsyncManagerExtensions
    {
        public static void SetResult(this AsyncManager asyncManager, object result)
        {
            asyncManager.Parameters["result"] = result;
            asyncManager.OutstandingOperations.Decrement();
        }
        public static void SetAsyncResult<TState>(this AsyncManager asyncManager, int message, TState state)
        {
            asyncManager.SetResult(new AsyncResult<TState>(message, state));
        }
    }
}