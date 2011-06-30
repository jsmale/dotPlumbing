using System;
using Cqrs.Sample.Commands;
using Cqrs.Sample.Domain;
using Cqrs.Sample.Events;
using NServiceBus;

namespace Cqrs.Sample.CommandService.Handlers
{
    public class RegisterUserCommandHandler : IHandleMessages<RegisterUserCommand>
    {
        readonly IBus bus;
        readonly IDomainRepository repository;

        public RegisterUserCommandHandler(IBus bus, IDomainRepository repository)
        {
            this.bus = bus;
            this.repository = repository;
        }

        public void Handle(RegisterUserCommand message)
        {
            var user = new User(message.UserName, message.Email, message.Password);
            repository.Save(user);
            bus.Return(RegisterUserCommandResult.Success);
        }
    }
}