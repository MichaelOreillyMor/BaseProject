using GFF.DatasMan;
using GFF.DatasMan.GameDatas;
using GFF.ServiceLocators;

using RPGGame.DatasMan;
using RPGGame.DatasMan.GameDatas;

namespace RPGGame.GameDatasMan
{
    public class RPGDataManager : DataManager, IRPGDataManager
    {
        private GameData gameData;

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<IRPGDataManager>(this);
        }

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
