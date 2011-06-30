using System;

namespace Cqrs.Sample.Events
{
    public class UserRegisteredEvent : IEvent
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
