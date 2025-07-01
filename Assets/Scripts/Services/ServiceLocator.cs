using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("ServiceLocator is not initialized.");
                }
                return _instance;
            }
        }

        public void RegisterService<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
        }

        internal ServiceLocator()
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("ServiceLocator is already initialized.");
            }
            _instance = this;
        }
    }
}