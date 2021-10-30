namespace GFFramework
{
    public interface IPlayerProvider
    {
        public void LoadPlayerController();
        public void UnloadPlayerController();
        public BasePlayerController GetPlayerController();
    }
}