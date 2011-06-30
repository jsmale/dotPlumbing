using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Plumbing.Container;

namespace Plumbing.Web.Mvc.Container
{
    public class ContainerDependencyResolver : IDependencyResolver
    {
        readonly IContainer container;

        public ContainerDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }
    }
}