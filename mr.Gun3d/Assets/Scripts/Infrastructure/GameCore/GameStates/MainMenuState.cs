using Infrastructure.Constants;
using Infrastructure.SceneManagment;
using Infrastructure.UI.UIElements;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using YG;

namespace Infrastructure.GameCore.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIService _uiService;

        public MainMenuState(IGameStateMachine stateMachine, ILoadingCurtain loadingCurtain, IUIService uiService,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _uiService = uiService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _uiService.Initialize();
            _sceneLoader.Load("MainMenu", MenuSceneLoaded);
        }

        public void Exit()
        {
            _uiService.DisableUI();
            _loadingCurtain.Show();
        }

        private void MenuSceneLoaded()
        {
            _uiService.EnableMainManu();

        }

        public void StartGame()
        {
            _stateMachine.EnterState<LoadLevelState>();
        }
    }
}