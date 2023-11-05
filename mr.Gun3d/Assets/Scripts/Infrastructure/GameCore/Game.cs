using Infrastructure.GameCore.GameLoopStates;
using Infrastructure.GameCore.GameStates;
using UnityEngine;

namespace Infrastructure.GameCore
{
    public class Game : IGame
    {
        private readonly IGameLoopStateMachine _gameLoopSateMachine;
        private readonly IGameStateMachine _gameStateMachine;

        public Game(IGameLoopStateMachine gameLoopSateMachine, IGameStateMachine gameStateMachine)
        {
            _gameLoopSateMachine = gameLoopSateMachine;
            _gameStateMachine = gameStateMachine;
            IsRuning = false;
        }

        public bool IsRuning { get; set; }

        public void Play()
        {
            IsRuning = true;
            _gameLoopSateMachine.EnterState<GameInitializingState>();
        }

        public void Exit()
        {
            IsRuning = false;
            _gameLoopSateMachine.EnterState<LevelClearingState>();
            _gameStateMachine.EnterState<ProgressSavingState>();
        }

        public void Restart()
        {
            _gameLoopSateMachine.EnterState<GameInitializingState>();
        }
    }
}