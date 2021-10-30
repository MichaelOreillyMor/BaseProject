namespace GFFramework
{
    public interface IDataProvider
    {
        public T GetGameData<T>() where T : BaseGameData;
        public T GetGameDataState<T>() where T : BaseGameDataState;
    }
}