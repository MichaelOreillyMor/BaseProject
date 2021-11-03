using GFFramework.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LoadScreen : BaseUIScreen
    {
        //TO-DO : shows a progress bar
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