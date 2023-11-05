using System;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.Random;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class LevelGenerationState : IGameLoopState
    {
        private readonly IGameLoopStateMachine _gameLoopStateMachine;
        private readonly ILevelGenerator _levelGenerator;
        private int _basicStatesCount;
        private readonly GenerationStaticData _settings;

        public LevelGenerationState(ILevelGenerator levelGenerator, IGameLoopStateMachine gameLoopStateMachine,
            GameSettingsProvider settings)
        {
            _levelGenerator = levelGenerator;
            _gameLoopStateMachine = gameLoopStateMachine;
            _settings = settings.GenerationSettings;
        }

        public void Enter()
        {
            _levelGenerator.UpdateLevel(MoveNextState);
        }

        private void MoveNextState()
        {
            if (_settings.basicStatesMaxCount < _basicStatesCount)
            {
                _gameLoopStateMachine.EnterState<RunningState>();
                _basicStatesCount = 0;
            }
            else
            {
                Debug.Log(_basicStatesCount);
                _gameLoopStateMachine.EnterState<EnitiesMovingState>();
                _basicStatesCount++;
            }

        }
    }
}