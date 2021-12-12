
namespace GFF.ServiceLocators
{
    public interface IGetService
    {
        public T GetService<T>() where T : class;
    }
}