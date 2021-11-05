using GFFramework.Enums;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFFramework.Scenes
{
    /// <summary>
    /// TO-DO
    /// </summary>
    public class SceneManager : BaseGameManager, ISceneProvider
    {
        #region IGameManager

        [Serializable]
        public struct SceneInfo
        {
            public SceneKey key;
            public string path;
        }

        [SerializeField]
        SceneInfo[] scenesToLoad;

        //If we dont use Odin we have to do this
        private Dictionary<SceneKey, string> scenes;

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SceneProv = this;

            LoadGameStates();
            //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("");
            //UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("");

            Debug.Log("Setup SceneManager");
            onNextSetup?.Invoke();
        }

        private void LoadGameStates()
        {
            if (scenesToLoad != null)
            {
                scenes = new Dictionary<SceneKey, string>();
                for (int i = 0; i < scenesToLoad.Length; i++)
                {
                    SceneInfo si = scenesToLoad[i];
                    scenes.Add(si.key, si.path);
                }
            }
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup SceneManager");
        }


        #endregion
    }
}
