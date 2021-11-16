using GFFramework;
using GFFramework.GameStates;
using GFFramework.UI;

using RPGGame.Units;

using UnityEngine;

namespace RPGGame.GameStates
{
    /// <summary>
    /// This the state that handles the load of a new a new GameSession.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSessionState")]
    public class LoadSessionGameState : BaseGameState
    {
        private IRPGDataProvider dataProv;
        private IRPGGameSessionProvider sessionProv;
        private IUIProvider UIProv;

        [SerializeField]
        private UnitStatesFactory unitStatesFactory;

        private LoadScreen loadScreen;

        #region Setup/Unsetup methods

        protected override void SetProviders(IGetProvidersRegister reg)
        {
            dataProv = (IRPGDataProvider)reg.DataProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
            UIProv = reg.UIProv;

            unitStatesFactory.Init(reg.PoolProv);
        }

        protected override void OnPostSetup()
        {
            loadScreen = UIProv.ShowLoadScreen(true);
            loadScreen.Setup();

            LoadGameSession();
            LoadNextGameState();
        }

        protected override void OnPreUnsetup()
        {
            loadScreen.Unsetup();
            UIProv.ShowLoadScreen(false);
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Session methods

        private void LoadGameSession()
        {
 
        }

        #endregion
    }
}