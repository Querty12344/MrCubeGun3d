using Infrastructure.UI.UIElements;
using Infrastructure.UI.UIInfrostructure;

namespace Infrastructure.GameCore.GameStates
{
    public class GameLoopState : IGameState
    {
        private readonly IGame _game;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIService _uiService;

        public GameLoopState(IGame game, IGameStateMachine stateMachine, IUIService uiService,
            ILoadingCurtain loadingCurtain)
        {
            _game = game;
            _stateMachine = stateMachine;
            _uiService = uiService;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _game.Play();
        }
        
    }
}