using System.Collections.Generic;
using System.Web.Mvc;
using Plumbing.Container;
using Plumbing.Extensions;

namespace Plumbing.Web.Mvc.Filters
{
    public class ContainerFilterAttributeProvider : FilterAttributeFilterProvider
    {
        private readonly IContainer container;

        public ContainerFilterAttributeProvider(IContainer container)
        {
            this.container = container;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            return BuildUpAttributes(attributes);
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            return BuildUpAttributes(attributes);
        }

        IEnumerable<FilterAttribute> BuildUpAttributes(IEnumerable<FilterAttribute> attributes)
        {
            if (container == null) return attributes;

            attributes.Each(attribute => container.BuildUp(attribute));
            return attributes;
        }
    }
}