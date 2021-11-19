using GFFramework.Pools;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework
{
    public interface IPoolProvider
    {
        public void Preload(PoolMember prefab, int qty = 1);
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T :  PoolMember;
        public void Despawn(PoolMember poolMember);
        public void DestroyPoolsMembers();
    }
}