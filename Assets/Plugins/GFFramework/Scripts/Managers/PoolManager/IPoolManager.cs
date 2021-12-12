using GFF.PoolsMan.Pools;

namespace GFF.PoolsMan
{
    public interface IPoolManager : ISpawnManager
    {
        public void Preload(PoolMember prefab, int qty = 1);
        public void Despawn(PoolMember poolMember);
        public void DestroyPoolsMembers();
    }
}