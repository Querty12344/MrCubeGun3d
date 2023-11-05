using CameraComponents;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using UnityEngine;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class LevelClearingState : IGameLoopState
    {
        private readonly ICameraFollow _cameraFollow;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameLoopStateMachine _gameLoopStateMachine;
        private readonly ILevelGenerator _levelGenerator;

        public LevelClearingState(ILevelGenerator levelGenerator,
            IGameLoopStateMachine gameLoopStateMachine,
            ICoroutineRunner coroutineRunner,
            ICameraFollow cameraFollow)
        {
            _levelGenerator = levelGenerator;
            _gameLoopStateMachine = gameLoopStateMachine;
            _coroutineRunner = coroutineRunner;
            _cameraFollow = cameraFollow;
        }

        public void Enter()
        {
            _cameraFollow.Deactivate();
            _coroutineRunner.StopAllCoroutines();
            _levelGenerator.ClearLevel();
        }
    }
}