using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates;
using GFFramework.UI;
using UnityEngine;

namespace RPGGame.GameStates
{
    /// <summary>
    /// This the base state that handles the load of a new scene and the dispose of the previous one.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/EnemyTurnGameState")]
    public class EnemyTurnGameState : BaseGameState
    {
        private IRPGGameSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetProviders(IGetProvidersRegister reg)
        {
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostSetup()
        {
            LoadNextGameState();
        }

        protected override void OnPreUnsetup()
        {

        }

        #endregion

        public override void Update()
        {

        }
    }
}