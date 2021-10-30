using UnityEngine;

namespace GFFramework
{
    public abstract class BasePlayerController : MonoBehaviour, IRequireInit
    {
        public bool IsInit { get; private set; }

        public virtual void Setup()
        {
            IsInit = true;
        }

        public virtual void Unetup()
        {
            IsInit = false;
        }
    }
}
