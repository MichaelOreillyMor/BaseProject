using GFFramework.Enums;
using GFFramework.Utils;

using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// Base class for UIScreens (Canvas + UI objects) that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// </summary>
    public abstract class BaseUIScreen : MonoBehaviour
    {
        /// <summary>
        /// GameState that owns this UIScreen
        /// </summary>
        public GameStateKey Owner => owner;

        [SerializeField, ReadOnly]
        private GameStateKey owner;
   

        /// <summary>
        /// Important to unregister the screen from any event that started to listen in the Setup()
        /// </summary>
        public abstract void Unsetup();

        public void Show(bool show) => gameObject.SetActive(show);

        public void SetOwner(GameStateKey gameStateKey)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                owner = gameStateKey;
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (gameObject)
                    {
                        UnityEditor.PrefabUtility.SavePrefabAsset(gameObject);
                    }  
                };
            }
#endif
        }
    }
}