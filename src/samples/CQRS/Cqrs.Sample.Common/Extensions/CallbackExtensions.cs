using System;
using System.Web.Mvc.Async;
using NServiceBus;

namespace Cqrs.Sample.Common.Extensions
{
    public static class CallbackExtensions
    {
        public static void RegisterAsyncResult<TState>(this ICallback callback,
            AsyncManager manager, TState state)
        {
            callback.Register(result => manager.SetAsyncResult(result, state));
        }
    }
}