using GFF.PoolsMan.Pools;

using UnityEngine;

namespace GFF.PoolsMan
{
    public interface ISpawnProvider
    {
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolMember;
    }
}