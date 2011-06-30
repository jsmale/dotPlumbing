using System;
using Cqrs.Sample.Events;
using Cqrs.Sample.ViewModels;
using NServiceBus;
using Plumbing.DataAccess;

namespace Cqrs.Sample.QueryService.Handlers
{
    public class UserRegisteredEventHandler : IHandleMessages<UserRegisteredEvent>
    {
        readonly IRepository repository;

        public UserRegisteredEventHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(UserRegisteredEvent message)
        {
            repository.Insert(new User(message.Id, message.UserName, message.Email));
            repository.SubmitChanges();
        }
    }
}