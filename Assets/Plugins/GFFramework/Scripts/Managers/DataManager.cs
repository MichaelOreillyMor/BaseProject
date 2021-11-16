using System;
using UnityEngine;

namespace GFFramework.GameDatas
{
    /// <summary>
    ///Handles the game´s datas, this class is just an example, in a real game would be resposible of the game DDBB
    /// </summary>
    public abstract class DataManager : BaseGameManager, IDataProvider
    {
        [SerializeField]
        private BaseGameData baseGameData;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.DataProv = this;

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

        /// <summary>
        /// Again just an example
        /// </summary>
        protected abstract void OnGameDataLoaded(BaseGameData baseGameData);

    }
}
