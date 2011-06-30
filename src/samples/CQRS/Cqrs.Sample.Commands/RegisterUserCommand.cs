using NServiceBus;

namespace Cqrs.Sample.Commands
{
    public class RegisterUserCommand : IMessage
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public static class RegisterUserCommandResult
    {
        public static int Success = 0;
        public static int UnknownError = 1;
        public static int ValidationError = 2;

        public static string ResultToError(int result)
        {
            if (result == UnknownError)
            {
                return "Unknown error occurred";
            }
            if (result == ValidationError)
            {
                return "Validation error occurred";
            }
            return null;
        }
    }
}