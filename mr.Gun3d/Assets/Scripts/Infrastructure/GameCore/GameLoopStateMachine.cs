using System;
using System.Collections.Generic;
using Infrastructure.GameCore.GameLoopStates;
using Zenject;

namespace Infrastructure.GameCore
{
    public class GameLoopStateMachine : IGameLoopStateMachine
    {
        private readonly DiContainer _container;
        private IGameLoopState _activeState;
        private Dictionary<Type, IGameLoopState> _gameLoopStates;

        public GameLoopStateMachine(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public void EnterState<TState>() where TState : IGameLoopState
        {
            if (_gameLoopStates == null) InitializeStates();

            _activeState = _gameLoopStates[typeof(TState)];
            _activeState.Enter();
        }

        public void InitializeStates()
        {
            var movingState = _container.Instantiate<EnitiesMovingState>();
            _container.Bind<EnitiesMovingState>().FromInstance(movingState).AsSingle();
            _gameLoopStates = new Dictionary<Type, IGameLoopState>
            {
                [typeof(EnitiesMovingState)] = movingState,
                [typeof(GameInitializingState)] = _container.Instantiate<GameInitializingState>(),
                [typeof(LevelGenerationState)] = _container.Instantiate<LevelGenerationState>(),
                [typeof(PlayerLoseState)] = _container.Instantiate<PlayerLoseState>(),
                [typeof(ShootingState)] = _container.Instantiate<ShootingState>(),
                [typeof(LevelClearingState)] = _container.Instantiate<LevelClearingState>(),
                [typeof(RunningState)] = _container.Instantiate<RunningState>()
            };
        }
    }
}