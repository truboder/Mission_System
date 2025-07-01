using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("ServiceLocator is not initialized.");
                }
                return instance;
            }
        }

        public void RegisterService<T>(T service)
        {
            services[typeof(T)] = service;
        }

        public T GetService<T>()
        {
            if (services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
        }

        internal ServiceLocator()
        {
            if (instance != null)
            {
                throw new InvalidOperationException("ServiceLocator is already initialized.");
            }
            instance = this;
        }
    }
}