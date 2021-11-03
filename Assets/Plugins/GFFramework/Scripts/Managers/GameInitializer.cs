using System;
using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Handles the game´s initialization and keeps a register of the managers
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        public BaseGameManager[] gameManagers;

        public ProvidersRegister reg;

        private void Awake() => Setup();

        private void Setup()
        {
            DontDestroyOnLoad(gameObject);
            reg = new ProvidersRegister();

            if (gameManagers != null && gameManagers.Length > 0)
            {
                gameManagers[0].Setup(reg, () => OnSetupComplete(0));
            }
        }

        private void OnSetupComplete(int indexManager)
        {
            indexManager++;

            if (indexManager < gameManagers.Length)
            {
                gameManagers[indexManager].Setup(reg, () => OnSetupComplete(indexManager));
            }
            else
            {
                OnGameLoaded();
            }
        }

        private void OnGameLoaded()
        {
            IGameStateProvider gameStateProv = reg.GameStateProv;
            gameStateProv.LoadInitGameState(reg);

            Debug.Log("OnGameLoaded");
        }

        private void Unsetup()
        {
            if (gameManagers != null)
            {
                for (int i = 0; i < gameManagers.Length; i++)
                {
                    gameManagers[i].Unsetup();
                }
            }
        }
    }
}