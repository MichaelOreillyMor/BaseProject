using GFFramework.PlayerControllers;
using GFFramework.Scenes;
using GFFramework.UI;
using System;
using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Handles the game´s initialization and keeps a register of the managers
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        private static GameInitializer instance;

        [SerializeField]
        private BaseGameManager[] gameManagers;

        private ProvidersRegister reg;

        private void Awake() => CheckSingleInstance();

        private void Start() => Setup();

        /// <summary>
        /// Just to be able to test scenes alone in the Unity Editor
        /// </summary>
        private void CheckSingleInstance()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

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
            LoadInitGameState();
        }

        private void LoadInitGameState()
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