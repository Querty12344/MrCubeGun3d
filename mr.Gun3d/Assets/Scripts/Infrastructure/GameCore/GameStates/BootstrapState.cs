using Infrastructure.Constants;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SceneManagment;
using Infrastructure.Sound;
using Infrastructure.UI.UIElements;
using YG;

namespace Infrastructure.GameCore.GameStates
{
    public class BootstrapState : IGameState
    {
        private readonly IGunAssetProvider _gunAssetProvider;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly StaticData _staticData;
        private readonly ISound _sound;

        public BootstrapState(IGameStateMachine stateMachine,
            ILoadingCurtain loadingCurtain,
            ISceneLoader sceneLoader,
            IGunAssetProvider gunAssetProvider,
            StaticData staticData,
            ISound sound)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _gunAssetProvider = gunAssetProvider;
            _staticData = staticData;
            _sound = sound;
        }

        public void Enter()
        {
            _staticData.LoadStaticData();
            _gunAssetProvider.InitializeGuns();
            _sound.Init();
            _sceneLoader.Load("Boot", BootSceneLoaded);
        }

        private void BootSceneLoaded()
        {
            _stateMachine.EnterState<MainMenuState>();
        }
    }
}