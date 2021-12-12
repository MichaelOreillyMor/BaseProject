using System;

namespace GFF.RegProviders
{
    public interface ISetService
    {
        public void SetService<T>(T service) where T : class;
    }
}
