using System;

namespace GFF.ServiceLocators
{
    public interface ISetService
    {
        public void SetService<T>(T service) where T : class;
    }
}
