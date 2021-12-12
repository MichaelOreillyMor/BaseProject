using GFF.PoolsMan.Pools;

using UnityEngine;

namespace GFF.PoolsMan
{
    public interface ISpawnManager
    {
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolMember;
    }
}