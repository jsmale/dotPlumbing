using System;
using NServiceBus;

namespace Cqrs.Sample.Commands
{
    public static class LogOnCommandResult
    {
        public static int Success = 0;
        public static int InvalidUsernameOrPassword = 1;
        public static int ValidationError = 2;

        public static string ResultToError(int result)
        {
            if (result == InvalidUsernameOrPassword)
            {
                return "Invalid Username or Password";
            }
            if (result == ValidationError)
            {
                return "Validation error occurred";
            }
            return null;
        }
    }

    public class LogOnCommand : IMessage
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }        
    }
}