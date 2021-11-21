using GFF.DatasMan;

using RPGGame.DatasMan.GameDatas;

namespace RPGGame.DatasMan
{
    public interface IRPGDataProvider : IDataProvider
    {
        public MapLevelData GetCurrentMapLevel();
    }
}