using System.Web.Mvc;
using Cqrs.Sample.Commands;
using Cqrs.Sample.Common;
using Cqrs.Sample.Common.Extensions;
using Cqrs.Sample.ViewModels;
using Cqrs.Sample.Web.Models;
using Cqrs.Sample.Web.Services;
using NServiceBus;
using System.Linq;
using Plumbing.DataAccess;

namespace Cqrs.Sample.Web.Controllers
{
    public class AccountController : AsyncController
    {
        readonly IFormsAuthenticationService formsService;
        readonly IBus bus;
        readonly IDataContext dataContext;

        public AccountController(IFormsAuthenticationService formsService, IBus bus, IDataContext dataContext)
        {
            this.formsService = formsService;
            this.bus = bus;
            this.dataContext = dataContext;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public void LogOnAsync(LogOnModel model)
        {
            AsyncManager.OutstandingOperations.Increment();
            if (!ModelState.IsValid)
            {
                AsyncManager.SetAsyncResult(LogOnCommandResult.ValidationError, model);
                return;
            }
            var user = dataContext.Query<User>().FirstOrDefault(x => x.Username == model.UserName);
            if (user == null)
            {
                AsyncManager.SetAsyncResult(LogOnCommandResult.InvalidUsernameOrPassword, model);
                return;
            }

            bus.Send(new LogOnCommand {UserId = user.Id, Password = model.Password})
                .RegisterAsyncResult(AsyncManager, model);
        }

        [HttpPost]
        public ActionResult LogOnCompleted(AsyncResult<LogOnModel> result)
        {
            var model = result.State;

            if (result.Message != LogOnCommandResult.Success)
            {
                ModelState.AddModelError("", 
                    LogOnCommandResult.ResultToError(result.Message));
                return View(model);
            }
            
            formsService.SignIn(model.UserName, model.RememberMe);
            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToHome();
        }

        RedirectToRouteResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            formsService.SignOut();

            return RedirectToHome();
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void RegisterAsync(RegisterModel model)
        {
            AsyncManager.OutstandingOperations.Increment();
            if (!ModelState.IsValid)
            {
                AsyncManager.SetAsyncResult(RegisterUserCommandResult.ValidationError, model);
                return;
            }

            bus.Send(new RegisterUserCommand { UserName = model.UserName, Password = model.Password, Email = model.Email })
                .RegisterAsyncResult(AsyncManager, model);            
        }

        [HttpPost]
        public ActionResult RegisterCompleted(AsyncResult<RegisterModel> result)
        {
            var model = result.State;

            if (result.Message != RegisterUserCommandResult.Success)
            {
                ModelState.AddModelError("",
                    RegisterUserCommandResult.ResultToError(result.Message));
                return View(model);
            }

            formsService.SignIn(model.UserName, false);
            return RedirectToHome();
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************
//
//        [Authorize]
//        public ActionResult ChangePassword()
//        {
//            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
//            return View();
//        }
//
//        [Authorize]
//        [HttpPost]
//        public ActionResult ChangePassword(ChangePasswordModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
//                {
//                    return RedirectToAction("ChangePasswordSuccess");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
//                }
//            }
//
            // If we got this far, something failed, redisplay form
//            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
//            return View(model);
//        }
//
        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************
//
//        public ActionResult ChangePasswordSuccess()
//        {
//            return View();
//        }

    }
}
