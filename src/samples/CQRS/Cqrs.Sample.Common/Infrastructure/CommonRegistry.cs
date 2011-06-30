using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.Common.Infrastructure
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            Scan(a =>
            {
                a.AssemblyContainingType<CommonRegistry>();
                a.WithDefaultConventions();
            });
        }
    }
}