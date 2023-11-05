using Infrastructure.GameCore.GameLoopStates;

namespace Infrastructure.GameCore
{
    public interface IGameLoopStateMachine
    {
        void EnterState<TState>() where TState : IGameLoopState;
    }
}