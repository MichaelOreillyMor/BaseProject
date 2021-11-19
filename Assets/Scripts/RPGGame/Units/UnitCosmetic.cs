using GFFramework.Pools;
using System;
using UnityEngine;

namespace RPGGame.Units
{
    /// <summary>
    /// Controls visual feedback of the Unit (animations, particles systems...)
    /// </summary>
    public class UnitCosmetic : PoolMember
    {
        [SerializeField]
        private Transform panelAnchor;

        [SerializeField]
        private ParticleSystem attackFX;

        [SerializeField]
        private ParticleSystem hitFX;

        public Transform GetPanelAnchor()
        {
            return panelAnchor;
        }

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

        public void Despawn() 
        {
            attackFX.Stop();
            hitFX.Stop();
            DespawnToPool();
        }
    }
}