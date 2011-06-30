using System;

namespace Cqrs.Sample.ViewModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        protected User()
        {
        }

        public User(Guid id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}
