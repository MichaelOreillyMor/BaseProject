using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// This keeps a reference to the UIScreens preloaded in this scene that will be persistent.
    /// </summary>
    [Serializable]
    public class RefsSceneUIScreens
    {
        public BaseUIScreen[] Screens => screens;

        [SerializeField]
        private BaseUIScreen[] screens;
    }
}