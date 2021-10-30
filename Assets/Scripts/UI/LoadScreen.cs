using GFFramework.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LoadScreen : BaseUIScreen
    {
        //TO-DO : shows a progress bar
        public override void Setup()
        {
            if (!IsInit)
            {
                //TO_DO
                base.Setup();
            }
        }

        public override void Unetup()
        {
            if (IsInit)
            {
                //TO_DO
                base.Unetup();
            }
        }
    }
}