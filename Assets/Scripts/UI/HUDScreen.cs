using GFFramework.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class HUDScreen : BaseUIScreen
    {
        public void Setup()
        {
            if (!IsInit)
            {
                //TO_DO
                OnSetup();
            }
        }

        protected override void OnUnsetup()
        {
            //TO_DO
        }
    }
}