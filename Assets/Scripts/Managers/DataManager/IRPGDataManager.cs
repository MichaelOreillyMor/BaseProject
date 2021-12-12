using GFF.DatasMan;

using RPGGame.DatasMan.GameDatas;

namespace RPGGame.DatasMan
{
    public interface IRPGDataManager : IDataManager
    {
        public MapLevelData GetCurrentMapLevel();
    }
}