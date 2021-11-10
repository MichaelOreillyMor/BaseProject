using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework.Pools
{
    [Serializable]
    public struct PreloadPoolMember
    {
        public int amount;
        public PoolMember prefab;
    }

    /// <summary>
    /// Handles the instantiation of objects in-game
    /// </summary>
    public class PoolManager : BaseGameManager, IPoolProvider
    {
        // You can avoid resizing of the Stack's internal data by
        // setting this to a number equal to or greater to what you
        // expect most of your pool sizes to be.
        // Note, you can also use Preload() to set the initial size
        // of a pool -- this can be handy if only some of your pools
        // are going to be exceptionally large (for example, your bullets.)
        private const int DEFAULT_POOL_SIZE = 3;

        // All of our pools
        private Dictionary<PoolMember, Pool> pools;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.PoolProv = this;

            Debug.Log("Setup PoolManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup PoolManager");
        }

        #endregion

        /// <summary>
        /// Initialize our dictionary.
        /// </summary>
        void InitPool(PoolMember prefab = null, int qty = DEFAULT_POOL_SIZE)
        {
            if (pools == null)
            {
                pools = new Dictionary<PoolMember, Pool>();
            }

            if (prefab != null && !pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new Pool(prefab, qty));
            }
        }

        public void PreloadPools(PreloadPoolMember[] preloadPoolMembers) 
        {
            if (preloadPoolMembers != null) 
            {
                for (int i = 0; i < preloadPoolMembers.Length; i++)
                {
                    PreloadPoolMember pm = preloadPoolMembers[i];

                    if (pm.prefab != null && pm.amount > 0)
                    {
                        Preload(pm.prefab, pm.amount);
                    }
                }
            }
        }

        /// <summary>
        /// If you want to preload a few copies of an object at the start
        /// of a scene, you can use this. Really not needed unless you're
        /// going to go from zero instances to 100+ very quickly.
        /// Could technically be optimized more, but in practice the
        /// Spawn/Despawn sequence is going to be pretty darn quick and
        /// this avoids code duplication.
        /// </summary>
        public void Preload(PoolMember prefab, int qty = 1)
        {
            InitPool(prefab, qty);

            // Make an array to grab the objects we're about to pre-spawn.
            PoolMember[] obs = new PoolMember[qty];
            for (int i = 0; i < qty; i++)
            {
                obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
            }

            // Now despawn them all.
            for (int i = 0; i < qty; i++)
            {
                Despawn(obs[i]);
            }
        }

        /// <summary>
        /// Spawns a copy of the specified prefab (instantiating one if required).
        /// NOTE: Remember that Awake() or Start() will only run on the very first
        /// spawn and that member variables won't get reset.  OnEnable will run
        /// after spawning -- but remember that toggling IsActive will also
        /// call that function.
        /// </summary>
        public PoolMember Spawn(PoolMember prefab, Vector3 pos, Quaternion rot)
        {
            InitPool(prefab);
            return pools[prefab].Spawn(pos, rot);
        }

        public List<PoolMember> GetActiveInstances(PoolMember prefab)
        {
            return pools[prefab].GetActiveInstances();
        }

        /// <summary>
        /// Despawn the specified gameobject back into its pool.
        /// </summary>
        public void Despawn(PoolMember poolMember)
        {
            if (poolMember.Pool == null)
            {
                Debug.Log("Object '" + poolMember.name + "' wasn't spawned from a pool. Destroying it instead.");
                Destroy(poolMember.gameObject);
            }
            else
            {
                poolMember.Pool.Despawn(poolMember);
            }
        }

        /// <summary>
        /// Destroys all the pools
        /// </summary>
        public void DestroyPoolsMembers() 
        {
            if (pools != null)
            {
                foreach (Pool pool in pools.Values)
                {
                    pool.DestroyPoolMembers();
                }

                pools.Clear();
            }
        }
    }
}