using GFFramework.PlayerControllers;
using GFFramework.Scenes;
using GFFramework.UI;

using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Handles the game´s initialization and keeps a register of the managers
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private BaseGameManager[] gameManagers;

        private ProvidersRegister reg;

        //Use Start for init the game, Awake() for scene GameObjects
        private void Start() => Setup();

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