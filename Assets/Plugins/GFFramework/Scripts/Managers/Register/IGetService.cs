
namespace GFF.RegProviders
{
    public interface IGetService
    {
        public T GetService<T>() where T : class;
    }
}