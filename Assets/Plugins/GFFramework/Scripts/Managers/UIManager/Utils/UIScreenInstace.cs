using GFF.UIsMan.UIScreens;
using UnityEngine;

namespace GFF.ScenesMan.Utils
{
    [System.Serializable]
    public struct UIScreenInstace
    {
        [Disable]
        public int PrefID;
        public BaseUIScreen UIScreen;

        public UIScreenInstace(int iD, BaseUIScreen uIScreen)
        {
            PrefID = iD;
            UIScreen = uIScreen;
        }
    }
}