using System;
using UnityEngine;

namespace GFFramework.Pools
{
    public class PoolMember : MonoBehaviour
    {
        private Pool pool;
        private Action<PoolMember, Pool> onReturnToPool;

        /// <summary>
        /// WARNING: Only the pool that owns this object should call this method.
        /// </summary>
        /// <param name="pool">Pool that owns it</param>
        /// <param name="onDespawnToPoolCallback">Callback to return this object to its pool</param>
        public void InitPoolMember(Pool pool, Action<PoolMember, Pool> onDespawnToPoolCallback) 
        {
            this.pool = pool;
            this.onReturnToPool = onDespawnToPoolCallback;
        }

        /// <summary>
        /// Despawn this object in the pool that owns it.
        /// </summary>
        protected void DespawnToPool()
        {
            if (onReturnToPool != null)
            {
                onReturnToPool(this, pool);
            }
            else
            {
                Debug.LogError("Can´t despawn, callback is null, please register the PoolMember first");
            }
        }

        public Pool GetPool()
        {
            return pool;
        }
    }
}