using GFFramework.Pools;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework
{
    public interface IPoolProvider
    {
        public void Preload(PoolMember prefab, int qty = 1);
        public PoolMember Spawn(PoolMember prefab, Vector3 pos, Quaternion rot);
        public List<PoolMember> GetActiveInstances(PoolMember prefab);
        public void Despawn(PoolMember poolMember);

        public void DestroyPoolsMembers();

        public void PreloadPools(PreloadPoolMember[] preloadPoolMembers);
    }
}