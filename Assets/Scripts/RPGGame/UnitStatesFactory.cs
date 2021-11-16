
using UnityEngine;

using GFFramework;

namespace RPGGame.Units
{
    [System.Serializable]
    public class UnitStatesFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        private IPoolProvider poolManager;

        public void Init(IPoolProvider poolManager)
        {
            this.poolManager = poolManager;
        }
    }
}