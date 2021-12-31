using GFF.Generated;
using GFF.ScenesMan.Keys;
using GFF.ServiceLocators;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFF.ScenesMan
{
    public class GameScenesManager : BaseGameManager, ISceneManager, IGenAssetsEnum
    {
        [SerializeField, SceneName]
        private string[] scenes;

        private Action onSceneLoadCallback;

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetupCallback)
        {
            SetService(serviceLocator);
            onNextSetupCallback?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<ISceneManager>(this);
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup SceneManager");
        }

        #endregion

        private string GetScene(SceneKey sceneKey)
        {
            int indexScene = ((int)sceneKey) - 1;

            if (scenes != null && indexScene < scenes.Length)
            {
                return scenes[indexScene];
            }

            Debug.Log(sceneKey.ToString() + " not found");
            return null;
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

        #region Editor methods

        public void SetAssetsEnum_Editor(List<UnityEngine.Object> assetsEnum)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                string[] scenes = Array.ConvertAll(assetsEnum.ToArray(), a => a.name);
                if (scenes != null)
                {
                    this.scenes = scenes;
                }
            }
#endif
        }

        public List<UnityEngine.Object> GetAssetsEnum_Editor()
        { 
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                List<UnityEngine.Object> sceneAssets = new List<UnityEngine.Object>();
                foreach (string sceneName in scenes)
                {
                    Scene scene = SceneManager.GetSceneByName(sceneName);
                    UnityEditor.SceneAsset sceneAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.SceneAsset>(scene.path);

                    if (sceneAsset) 
                    {
                        sceneAssets.Add(sceneAsset);
                    }
                }

                return sceneAssets;
            }
#endif

            return null;
        }

        public UnityEngine.Object GetAssetObject_Editor()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return this;
            }
#endif

            return null;
        }

        #endregion

    }
}
