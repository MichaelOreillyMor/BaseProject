using GFFramework.Enums;

using System.Collections;

namespace GFFramework
{
    public interface IGameStateProvider
    {
        public void LoadInitGameState(IGetProvidersRegister reg);
        public void LoadGameState(GameStateKey gameStateKey);
        public void LoadPrevGameState();

        public void StartStateCoroutine(IEnumerator coroutine);
    }
}