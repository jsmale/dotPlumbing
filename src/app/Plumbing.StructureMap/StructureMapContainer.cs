using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;

namespace Plumbing.StructureMap
{
    public class StructureMapContainer : Container.IContainer
    {
        readonly IContainer container;

        public StructureMapContainer(IContainer container)
        {
            this.container = container;
        }

        public IContainer InnerContainer
        {
            get { return container; }
        }

        public object GetInstance(Type type)
        {
            return container.GetInstance(type);
        }

        public T GetInstance<T>()
        {
            return container.GetInstance<T>();
        }

        public object GetNamedInstance(Type type, string instanceName)
        {
            return container.GetInstance(type, instanceName);
        }

        public T GetNamedInstance<T>(string instanceName)
        {
            return container.GetInstance<T>(instanceName);
        }

        public object TryGetInstance(Type type)
        {
            return container.TryGetInstance(type);
        }

        public T TryGetInstance<T>()
        {
            return container.TryGetInstance<T>();
        }

        public object TryGetNamedInstance(Type type, string instanceName)
        {
            return container.TryGetInstance(type, instanceName);
        }

        public T TryGetNamedInstance<T>(string instanceName)
        {
            return container.TryGetInstance<T>(instanceName);
        }

        public IEnumerable<object> GetAllInstances(Type type)
        {
            return container.GetAllInstances(type).Cast<object>();
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return container.GetAllInstances<T>();
        }

        public void BuildUp(object target)
        {
            container.BuildUp(target);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            container.Configure(x => x.For<TInterface>().Use<TImplementation>());
        }
    }
}