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
    public interface IGetProvidersRegister
    {
        public IGameStateProvider GameStateProv { get; }
        public ISceneProvider SceneProv { get; }
        public IDataProvider DataProv { get; }
        public IUIProvider UIProv { get; }
        public IInputProvider InputProv { get; }
        public IPoolProvider PoolProv { get; }
        public ICameraProvider CameraProv { get; }
        public ICoroutinesProvider CoroutinesProv { get; }
        public IGameSessionProvider GameSessionProv { get; }
    }
}