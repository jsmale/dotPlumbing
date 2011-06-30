using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.Web.Infrastructure
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(a =>
            {
                a.AssemblyContainingType<WebRegistry>();
                a.WithDefaultConventions();
            });
        }
    }
}