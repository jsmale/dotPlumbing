using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Plumbing.Container;
using Plumbing.Initialization;
using Plumbing.Web.Mvc.Container;
using Plumbing.Web.Mvc.Filters;

namespace Plumbing.Web.Mvc
{
    public abstract class PlumblingMvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Initializer.Initialize(GetInitializer());
            SetDependencyResolver();            
            AreaRegistration.RegisterAllAreas();
            SetupFilterProviders(FilterProviders.Providers);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected virtual void SetupFilterProviders(FilterProviderCollection providers)
        {
            var container = IoC.Container;
            if (container == null) return;

            var attributeFilterProvider = providers
                .Single(f => f is FilterAttributeFilterProvider);
            providers.Remove(attributeFilterProvider);
            providers.Add(new ContainerFilterAttributeProvider(container));
        }

        protected virtual void SetDependencyResolver()
        {
            var container = IoC.Container;
            if (container == null) return;

            container.Register<IControllerFactory, ContainerControllerFactory>();
            DependencyResolver.SetResolver(new ContainerDependencyResolver(container));
        }

        protected abstract IInitializer GetInitializer();

        protected virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            if (IoC.Container == null) return;
            filters.Add(IoC.Container.GetInstance<DataContextFilter>());
        }

        protected virtual void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}