using System;
using UnityEngine;

namespace GFFramework.GameDatas
{
    /// <summary>
    ///Handles the game´s datas, the models needed to initialize the controllers (MVC)
    /// </summary>
    public class DataManager : BaseGameManager, IDataProvider
    {
        [SerializeField]
        private BaseGameData baseGameData;
        private BaseGameDataState baseGameDataState;

        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.DataProv = this;
            baseGameDataState = baseGameData.GetGameDataState();

            Debug.Log("Setup DataManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup DataManager");
        }

        #endregion

        public T GetGameData<T>() where T : BaseGameData
        {
            return (T)baseGameData;
        }

        public T GetGameDataState<T>() where T : BaseGameDataState
        {
            return (T)baseGameDataState;
        }
    }
}
