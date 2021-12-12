using UnityEngine;

namespace GFF.PoolsMan.Pools
{
    [System.Serializable]
    public class SpawnFactory
    {
        private ISpawnManager spawnMan;

        protected void SetSpawner(ISpawnManager spawnMan)
        {
            this.spawnMan = spawnMan;
        }

        public T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : PoolMember
        {
            return spawnMan.Spawn(prefab, pos, rot);
        }
    }
}