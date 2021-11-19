using GFFramework.Enums;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFFramework.Scenes
{
    public class GameScenesManager : BaseGameManager, ISceneProvider
    {
        [Serializable]
        private struct SceneInfo
        {
            public SceneKey key;
            public string name;
        }

        //This can be done automatically generating the enums from the Scenes folder on Editor-time
        [SerializeField]
        private SceneInfo[] scenesToLoad;
        private Dictionary<SceneKey, string> scenes;

        private Action onSceneLoadCallback;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetupCallback)
        {
            reg.SceneProv = this;

            LoadSGamecenes();

            Debug.Log("Setup SceneManager");
            onNextSetupCallback?.Invoke();
        }

        private void LoadSGamecenes()
        {
            if (scenesToLoad != null)
            {
                scenes = new Dictionary<SceneKey, string>();

                for (int i = 0; i < scenesToLoad.Length; i++)
                {
                    SceneInfo si = scenesToLoad[i];
                    scenes.Add(si.key, si.name);
                }
            }
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup SceneManager");
        }

        #endregion

        private string GetScene(SceneKey sceneKey)
        {
            string sceneName;
            scenes.TryGetValue(sceneKey, out sceneName);

            if (sceneName == null)
            {
                Debug.Log(sceneKey.ToString() + " not found, please add the scene to the build settings");
            }

            return sceneName;
        }

        #region Load scene methods

        public void LoadScene(SceneKey sceneKey, Action onSceneLoadCallback, bool canReloadSameScene = false)
        {
            string sceneToLoad = GetScene(sceneKey);
            Scene currentScene = SceneManager.GetActiveScene();

            if (sceneToLoad != null)
            {
                this.onSceneLoadCallback = onSceneLoadCallback;

                if (canReloadSameScene || (sceneToLoad != currentScene.name))
                {
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
                    asyncLoad.completed += OnSceneLoaded;
                }
                else 
                {
                    onSceneLoadCallback?.Invoke();
                }
            }
        }

        private void OnSceneLoaded(AsyncOperation asyncLoad)
        {
            onSceneLoadCallback?.Invoke();
            onSceneLoadCallback = null;
        }

        #endregion
    }
}
