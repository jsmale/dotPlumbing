using StructureMap.Configuration.DSL;

namespace Cqrs.Sample.CommandService.Infrastructure
{
    public class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            Scan(a =>
            {
                a.AssemblyContainingType<CommandRegistry>();
                a.WithDefaultConventions();
            });
        }
    }
}