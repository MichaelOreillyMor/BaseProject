namespace GFFramework
{
    public interface IRequireInit
    {
        bool IsInit { get; }

        void Setup();
        void Unsetup();
    }
}