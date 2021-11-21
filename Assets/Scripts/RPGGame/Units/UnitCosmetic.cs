using GFF.PoolsMan.Pools;

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

        private Transform mainCameraTr;

        private Vector3 lastPosCamera;
        private Vector3 posAnchor;
        private Vector3 dirCam;

        public void Setup(Transform mainCameraTr)
        {
            this.mainCameraTr = mainCameraTr;
        }

        public void Unsetup()
        {
            attackFX.Stop();
            hitFX.Stop();
            DespawnToPool();
        }

        #region Animator methods

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

        #endregion

        #region Panel anchor methods

        private void UpdateAnchorPanelPos()
        {
            dirCam = (mainCameraTr.position - transform.position).normalized;
            posAnchor = transform.position + dirCam;
            panelAnchor.position = posAnchor;
        }

        public Transform GetPanelAnchor()
        {
            return panelAnchor;
        }

        private void Update()
        {
            if (lastPosCamera != mainCameraTr.position)
            {
                UpdateAnchorPanelPos();
            }
        }

        #endregion
    }
}