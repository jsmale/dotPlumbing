using System;
using System.Security.Principal;
using Cqrs.Sample.Events;
using Plumbing.Security;

namespace Cqrs.Sample.Domain
{
    public class User : AggregateRoot, IIdentity
    {
        string name;
        PasswordHandler passwordHandler;

        public User()
        {
        }

        public User(string name, string email, string password)
        {
            var newPasswordHandler = new PasswordHandler(password);
            ApplyChange(new UserRegisteredEvent
            {
                Id = GuidComb(),
                Email = email,
                UserName = name,
                PasswordHash = newPasswordHandler.PasswordHash,
                Salt = newPasswordHandler.Salt
            });
        }

        void Apply(UserRegisteredEvent e)
        {
            Id = e.Id;
            name = e.UserName;
            passwordHandler = new PasswordHandler(e.PasswordHash, e.Salt);
        }

        void Apply(UserAttemptedAuthenticationEvent e)
        {
        }

        public string Name
        {
            get { return name; }
        }

        public string AuthenticationType
        {
            get { return "Cqrs.Sample.Authentication"; }
        }

        public bool IsAuthenticated { get; private set; }

        public bool Authenticate(string password)
        {
            IsAuthenticated = passwordHandler.PasswordMatches(password);
            ApplyChange(new UserAttemptedAuthenticationEvent(Id, IsAuthenticated));
            return IsAuthenticated;
        }
    }
}