using GFFramework;

using RPGGame.GameDatas;

namespace RPGGame
{
    public interface IRPGDataProvider : IDataProvider
    {
        public MapLevelData GetCurrentMapLevel();
    }
}