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
    public class ProvidersRegister : IGetProvidersRegister, ISetProvidersRegister
    {
        public IGameStateProvider GameStateProv { get; set; }
        public ISceneProvider SceneProv { get; set; }
        public IDataProvider DataProv { get; set; }
        public IUIProvider UIProv { get; set; }
        public IInputProvider InputProv { get; set; }
        public IPoolProvider PoolProv { get; set; }
        public ICameraProvider CameraProv { get; set; }
        public ICoroutinesProvider CoroutinesProv { get; set; }
        public IGameSessionProvider GameSessionProv { get; set; }
    }
}