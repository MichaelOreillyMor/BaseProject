using GFFramework.PlayerControllers;

namespace GFFramework
{
    public interface IPlayerProvider
    {
        public void RegisterScenePlayerCharacter(BasePlayerCharacter playerCharacter);
        public BasePlayerCharacter GetPlayerCharacter();
        public void CleanPlayerCharacter();
    }
}