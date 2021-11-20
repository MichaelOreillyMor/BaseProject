
using UnityEngine;

namespace GFFramework.Pools
{
    [System.Serializable]
    public class SpawnFactory
    {
        private ISpawnProvider spawnProv;

        protected void SetSpawner(ISpawnProvider spawnProv)
        {
            this.spawnProv = spawnProv;
        }

        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolMember
        {
            return spawnProv.Spawn(prefab, pos, rot);
        }
    }
}