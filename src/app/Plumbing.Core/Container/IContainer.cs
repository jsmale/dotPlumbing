using System;
using System.Collections.Generic;

namespace Plumbing.Container
{
    public interface IContainer
    {
        object GetInstance(Type type);
        T GetInstance<T>();
        object GetNamedInstance(Type type, string instanceName);
        T GetNamedInstance<T>(string instanceName);

        object TryGetInstance(Type type);
        T TryGetInstance<T>();
        object TryGetNamedInstance(Type type, string instanceName);
        T TryGetNamedInstance<T>(string instanceName);

        IEnumerable<object> GetAllInstances(Type type);
        IEnumerable<T> GetAllInstances<T>();
        void BuildUp(object target);
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
    }
}