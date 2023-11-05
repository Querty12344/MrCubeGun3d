using Infrastructure.ScoreSystem;
using Infrastructure.UI.UIInfrostructure;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class PlayerLoseState : IGameLoopState
    {
        private readonly IUIService _ui;
        private readonly IScore _score;

        public PlayerLoseState(IUIService ui,IScore score)
        {
            _ui = ui;
            _score = score;
        }

        public void Enter()
        {
            _score.SaveScore();
            _ui.EnableEndGameInterface();
        }
    }
}