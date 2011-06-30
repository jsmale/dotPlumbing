using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.QueryService.Infrastructure
{
    public class QueryServiceRegistry : Registry
    {
        public QueryServiceRegistry()
        {
            Scan(a =>
            {
                a.AssemblyContainingType<QueryServiceRegistry>();
                a.WithDefaultConventions();
            });
        }
    }
}