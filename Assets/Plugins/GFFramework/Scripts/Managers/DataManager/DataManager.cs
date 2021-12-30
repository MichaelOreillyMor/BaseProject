using GFF.DatasMan.GameDatas;
using GFF.ServiceLocators;

using System;
using UnityEngine;

namespace GFF.DatasMan
{
    /// <summary>
    ///Handles the game´s datas, this class is just an example, in a real game would be resposible of the game DDBB
    /// </summary>
    public abstract class DataManager : BaseGameManager, IDataManager
    {
        [SerializeField]
        private BaseGameData baseGameData;

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetup)
        {
            SetService(serviceLocator);

            OnGameDataLoaded(baseGameData, onNextSetup);
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup DataManager");
        }

        #endregion

        public BaseGameData GetBaseGameData()
        {
            return baseGameData;
        }

    protected abstract void OnGameDataLoaded(BaseGameData baseGameData, Action onDataLoaded);
    }
}
