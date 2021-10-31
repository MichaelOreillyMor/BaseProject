using UnityEngine;

namespace GFFramework.PlayerControlles
{
    /// <summary>
    /// The GameObject controlled directly by the player
    /// </summary>
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
