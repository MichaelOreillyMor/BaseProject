using GFF.CamerasMan;
using GFF.CoroutinesMan;
using GFF.DatasMan;
using GFF.GameStatesMan;
using GFF.InputsMan;
using GFF.PoolsMan;
using GFF.ScenesMan;
using GFF.SessionsMan;
using GFF.UIsMan;

namespace GFF.RegProviders
{
    public interface ISetProvidersRegister
    {
        public IGameStateProvider GameStateProv { set; }
        public ISceneProvider SceneProv { set; }
        public IDataProvider DataProv { set; }
        public IUIProvider UIProv { set; }
        public IInputProvider InputProv { set; }
        public IPoolProvider PoolProv { set; }
        public ICameraProvider CameraProv { set; }
        public ICoroutinesProvider CoroutinesProv { set; }
        public IGameSessionProvider GameSessionProv { set; }
    }
}
