using System;

namespace Cqrs.Sample.ViewModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }

        protected User()
        {
        }

        public User(Guid id, string username, string email, string passwordHash)
        {
            Id = id;
            Username = username;
            Email = email;
        	PasswordHash = passwordHash;
        }
    }
}
