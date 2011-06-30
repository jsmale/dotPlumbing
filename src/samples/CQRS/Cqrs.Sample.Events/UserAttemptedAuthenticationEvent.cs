using System;

namespace Cqrs.Sample.Events
{
    public class UserAttemptedAuthenticationEvent : IEvent
    {
        public UserAttemptedAuthenticationEvent(Guid id, bool successfulAuthentication)
        {
            Id = id;
            SuccessfulAuthentication = successfulAuthentication;
        }

        public Guid Id { get; private set; }
        public bool SuccessfulAuthentication { get; private set; }
    }
}