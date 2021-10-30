using GFFramework;

using UnityEngine;

namespace Game
{
    public class PlayerController : BasePlayerController
    {
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
