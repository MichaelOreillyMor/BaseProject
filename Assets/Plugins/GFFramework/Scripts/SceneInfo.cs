using GFFramework.UI;

using UnityEngine;

namespace GFFramework.Scenes
{
    /// <summary>
    /// Contains references to assets preloaded in the scene and some information about the scene itself.
    /// Each scene should have only one.
    /// </summary>
    public class SceneInfo : MonoBehaviour
    {
        public static SceneInfo SceneRef { get; private set; }

        /// <summary>
        /// UIScreens that are persistent during the life time of this scene
        /// </summary>
        public BaseUIScreen[] SceneScreens => refsUIScreens.Screens;

        [SerializeField]
        private RefsSceneUIScreens refsUIScreens;

        private void Awake() => SceneRef = this;

    }
}