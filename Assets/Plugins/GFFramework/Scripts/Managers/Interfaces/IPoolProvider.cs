using GFFramework.Pools;

namespace GFFramework
{
    public interface IPoolProvider : ISpawnProvider
    {
        public void Preload(PoolMember prefab, int qty = 1);
        public void Despawn(PoolMember poolMember);
        public void DestroyPoolsMembers();
    }
}