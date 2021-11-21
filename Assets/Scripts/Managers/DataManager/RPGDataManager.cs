using GFF.DatasMan;
using GFF.DatasMan.GameDatas;

using RPGGame.DatasMan;
using RPGGame.DatasMan.GameDatas;

namespace RPGGame.GameDatasMan
{
    public class RPGDataManager : DataManager, IRPGDataProvider
    {
        private GameData gameData;

        protected override void OnGameDataLoaded(BaseGameData baseGameData)
        {
            gameData = (GameData)baseGameData;
        }

        public MapLevelData GetCurrentMapLevel()
        {
            return gameData.MapLevelDatas[0];
        }
    }
}
