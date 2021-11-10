using System;
using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// This keeps a reference to the UIScreens preloaded in this scene that will be persistent during the life time of the scene.
    /// </summary>
    [Serializable]
    public class RefsSceneUIScreens
    {
        public BaseUIScreen[] Screens => screens;

        [SerializeField]
        private BaseUIScreen[] screens;
    }
}