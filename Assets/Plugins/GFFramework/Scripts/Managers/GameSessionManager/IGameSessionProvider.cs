namespace GFF.SessionsMan
{
    public interface IGameSessionProvider
    {
        public void ResumeGame();
        public void StopGame();
        public void EndSession();
    }
}