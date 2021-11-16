using GFFramework.GameDatas;

namespace RPGGame.GameDatas
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
