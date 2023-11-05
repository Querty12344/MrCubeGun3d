using Infrastructure.GameCore.GameStates;

namespace Infrastructure.Factories
{
    public interface IGameStatesHandler
    {
        IGameState GetState<TState>();
    }
}