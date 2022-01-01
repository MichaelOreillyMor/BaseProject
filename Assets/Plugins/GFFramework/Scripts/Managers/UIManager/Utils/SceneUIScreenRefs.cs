using GFF.UIsMan.UIScreens;

using System.Collections.Generic;
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
        public UIScreenInstace[] UIScreens => uiScreens;

        [SerializeField]
        private UIScreenInstace[] uiScreens;

        private void Awake() => SceneRef = this;

        private void OnValidate()
        {
            SetUIScreenInstaces_Editor();
        }

        private void SetUIScreenInstaces_Editor()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                BaseUIScreen[] screens = transform.GetComponentsInChildren<BaseUIScreen>(true);
                List<UIScreenInstace> instances = new List<UIScreenInstace>();

                foreach (BaseUIScreen screen in screens)
                {
                    if (screen)
                    {
                        string prefPath = UnityEditor.PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(screen);

                        if (prefPath != string.Empty)
                        {
                            BaseUIScreen screenPref = (BaseUIScreen)UnityEditor.AssetDatabase.LoadAssetAtPath(prefPath, typeof(BaseUIScreen));

                            if (screen)
                            {
                                UIScreenInstace screenInstace = new UIScreenInstace(screenPref.GetInstanceID(), screen);
                                instances.Add(screenInstace);
                            }
                        }
                    }
                }

                uiScreens = instances.ToArray();
            }
#endif
        }
    }
}