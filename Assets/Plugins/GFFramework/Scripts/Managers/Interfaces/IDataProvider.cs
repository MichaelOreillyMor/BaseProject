using GFFramework.GameDatas;

namespace GFFramework
{
    public interface IDataProvider
    {
        public T GetGameData<T>() where T : BaseGameData;
    }
}