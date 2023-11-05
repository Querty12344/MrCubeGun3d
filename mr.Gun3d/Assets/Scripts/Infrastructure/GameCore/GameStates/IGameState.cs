namespace Infrastructure.GameCore.GameStates
{
    public interface IGameState
    {
        void Enter();

        void Exit()
        {
        }
    }
}