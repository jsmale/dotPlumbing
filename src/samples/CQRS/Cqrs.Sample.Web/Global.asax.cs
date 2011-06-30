using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cqrs.Sample.Web.Infrastructure;
using Plumbing.Initialization;
using Plumbing.Web.Mvc;

namespace Cqrs.Sample.Web
{
    public class MvcApplication : PlumblingMvcApplication
    {
        protected override IInitializer GetInitializer()
        {
            return new WebInitializer(Server.MapPath("~/"));
        }
    }
}