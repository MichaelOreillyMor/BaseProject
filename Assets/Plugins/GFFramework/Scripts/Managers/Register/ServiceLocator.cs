using System;
using System.Collections.Generic;

namespace GFF.ServiceLocators
{
    public class ServiceLocator : IGetService, ISetService
    {
        public Dictionary<object, object> services;

        public ServiceLocator()
        {
            this.services = new Dictionary<object, object>();
        }

        public T GetService<T>() where T : class
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object service))
            {
                return (T)service;
            }

            return null;
        }

        public void SetService<T>(T service) where T : class
        {
            Type type = typeof(T);

            if (!services.ContainsKey(type))
            {
                services.Add(type, service);
            }
        }
    }
}