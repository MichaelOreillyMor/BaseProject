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
        private int indexLoaded;

        private void Awake() => CheckSingleInstance();

        private void Start() => Setup();

        private void OnDestroy() => Unsetup();

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
            indexLoaded = 0;

            if (gameManagers != null && gameManagers.Length > 0)
            {
                gameManagers[indexLoaded].Setup(reg, OnSetupComplete);
            }
        }

        private void OnSetupComplete()
        {
            indexLoaded++;

            if (indexLoaded < gameManagers.Length)
            {
                gameManagers[indexLoaded].Setup(reg, OnSetupComplete);
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
                    BaseGameManager man = gameManagers[i];

                    if (man != null)
                    {
                        man.Unsetup();
                    }
                }
            }
        }
    }
}