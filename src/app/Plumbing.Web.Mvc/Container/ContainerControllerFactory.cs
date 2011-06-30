using System;
using System.Web.Mvc;
using System.Web.Routing;
using Plumbing.Container;

namespace Plumbing.Web.Mvc.Container
{
    public class ContainerControllerFactory : DefaultControllerFactory
    {
        readonly IContainer container;

        public ContainerControllerFactory(IContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;

            return (IController) container.GetInstance(controllerType);
        }
    }
}