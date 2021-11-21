using GFF.UIsMan.UIScreens;

using UnityEngine;

namespace GFF.ScenesMan.Utils
{
    /// <summary>
    /// Contains references to UIScreens preloaded in the scene.
    /// Each scene should have only one.
    /// </summary>
    public class SceneUIScreenRefs : MonoBehaviour
    {
        public static SceneUIScreenRefs SceneRef { get; private set; }

        /// <summary>
        /// UIScreens that are persistent during the life time of this scene
        /// </summary>
        public BaseUIScreen[] UIScreens => uiScreens;

        [SerializeField]
        private BaseUIScreen[] uiScreens;

        private void Awake() => SceneRef = this;

    }
}