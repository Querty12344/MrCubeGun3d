using Infrastructure.Factories;
using Infrastructure.GameCore.GameStates;

namespace Infrastructure.GameCore
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IGameStatesHandler _stateHandler;
        private IGameState _activeState;

        public GameStateMachine(IGameStatesHandler stateHandler)
        {
            _stateHandler = stateHandler;
        }

        public void EnterState<TState>() where TState : IGameState
        {
            _activeState?.Exit();
            _activeState = _stateHandler.GetState<TState>();
            _activeState.Enter();
        }
    }
}