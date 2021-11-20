using GFFramework.Pools;
using UnityEngine;

namespace GFFramework
{
    public interface ISpawnProvider
    {
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolMember;
    }
}