using GFF.Enums;
using GFF.ServiceLocators;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFF.ScenesMan
{
    public class GameScenesManager : BaseGameManager, ISceneManager
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

        public override void Setup(ISetService serviceLocator, Action onNextSetupCallback)
        {
            SetService(serviceLocator);

            LoadSGamecenes();

            Debug.Log("Setup SceneManager");
            onNextSetupCallback?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<ISceneManager>(this);
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
