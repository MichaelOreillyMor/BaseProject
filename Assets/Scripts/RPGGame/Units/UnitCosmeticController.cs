using GFFramework.Pools;
using UnityEngine;

namespace RPGGame.Units
{
    /// <summary>
    /// Controls visual feedback of the Unit (animations, particles systems...)
    /// </summary>
    public class UnitCosmeticController : PoolMember
    {
        [SerializeField]
        private ParticleSystem attackFX;

        [SerializeField]
        private ParticleSystem hitFX;

        public void PlayAttack()
        {
            //Just to show feedback, this should play an animation
            attackFX.Play();
        }

        public void PlayHit()
        { 
            //Just to show feedback, this should play an animation
            hitFX.Play();
        }

        public void PlayMove()
        {

        }
    }
}