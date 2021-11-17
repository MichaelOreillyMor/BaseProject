
namespace GFFramework.Pools
{
    [System.Serializable]
    public class PoolFactory
    {
        protected IPoolProvider PoolManager { get; private set; }

        public void SetPool(IPoolProvider poolManager)
        {
            this.PoolManager = poolManager;
        }
    }
}