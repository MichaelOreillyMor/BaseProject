namespace GFF.SessionsMan
{
    public interface IGameSessionManager
    {
        public void ResumeGame();
        public void StopGame();
        public void EndSession();
    }
}