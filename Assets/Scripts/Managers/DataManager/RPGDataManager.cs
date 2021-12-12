using GFF.DatasMan;
using GFF.DatasMan.GameDatas;
using GFF.RegProviders;

using RPGGame.DatasMan;
using RPGGame.DatasMan.GameDatas;

namespace RPGGame.GameDatasMan
{
    public class RPGDataManager : DataManager, IRPGDataProvider
    {
        private GameData gameData;

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<IRPGDataProvider>(this);
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
