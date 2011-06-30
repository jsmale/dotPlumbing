using System;

namespace Cqrs.Sample.Common
{
    public class AsyncResult<TState>
    {
        public AsyncResult(int message, TState state)
        {
            Message = message;
            State = state;
        }

        public TState State { get; set; }
        public int Message { get; set; }
    }
}