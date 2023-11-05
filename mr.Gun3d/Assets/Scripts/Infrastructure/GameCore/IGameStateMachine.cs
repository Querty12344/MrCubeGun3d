using Infrastructure.GameCore.GameStates;

namespace Infrastructure.GameCore
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IGameState;
    }
}