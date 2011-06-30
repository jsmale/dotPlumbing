using Cqrs.Sample.Commands;
using Cqrs.Sample.Domain;
using NServiceBus;

namespace Cqrs.Sample.CommandService.Handlers
{
    public class LogOnCommandHandler : IHandleMessages<LogOnCommand>
    {
        readonly IDomainRepository repository;
        readonly IBus bus;

        public LogOnCommandHandler(IDomainRepository repository, IBus bus)
        {
            this.repository = repository;
            this.bus = bus;
        }

        public void Handle(LogOnCommand message)
        {
            var user = repository.GetById<User>(message.UserId);
            var authenticated = user != null && user.Authenticate(message.Password);
            if (user != null) repository.Save(user);
            if (!authenticated)
            {
                bus.Return(LogOnCommandResult.InvalidUsernameOrPassword);
                return;
            }

            bus.Return(LogOnCommandResult.Success);
        }
    }
}