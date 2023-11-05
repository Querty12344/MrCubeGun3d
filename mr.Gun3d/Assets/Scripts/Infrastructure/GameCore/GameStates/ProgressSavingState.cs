using Infrastructure.MoneyManagement;
using Infrastructure.ScoreSystem;

namespace Infrastructure.GameCore.GameStates
{
    public class ProgressSavingState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IScore _score;
        private readonly IMoney _money;

        public ProgressSavingState(IGameStateMachine gameStateMachine,IScore score,IMoney money)
        {
            _gameStateMachine = gameStateMachine;
            _score = score;
            _money = money;
        }

        public void Enter()
        {
            _money.SaveMoney();
            _score.SaveScore();
            _gameStateMachine.EnterState<MainMenuState>();
        }
    }
}