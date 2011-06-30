using System;
using Machine.Specifications;
using Plumbing.Security;

namespace Plumbing.Core.Specs.Security
{
    public class PasswordHandlerSpecs
    {
        public abstract class concern
        {
            Establish c = () =>
            {
                password = Guid.NewGuid().ToString();
                passwordHandler = new PasswordHandler(password);
            };

            protected static PasswordHandler passwordHandler;
            protected static string password;
            protected static bool result;
        }

        [Subject(typeof(PasswordHandler))]
        public class when_authenticating_with_valid_password : concern
        {            
            Because b = () =>
                result = passwordHandler.PasswordMatches(password);

            It should_return_true = () =>
                result.ShouldBeTrue();
        }

        [Subject(typeof(PasswordHandler))]
        public class when_authenticating_with_invalid_password : concern
        {
            Because b = () =>
                result = passwordHandler.PasswordMatches(password.Substring(0, password.Length - 1));

            It should_return_false = () =>
                result.ShouldBeFalse();
        }
    }
}