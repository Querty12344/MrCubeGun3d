using System;
using System.Collections.Generic;
using Infrastructure.GameCore.GameStates;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameStatesHandler : IGameStatesHandler
    {
        private readonly DiContainer _container;
        private Dictionary<Type, IGameState> _allStates;

        public GameStatesHandler(DiContainer container)
        {
            _container = container;
        }

        public IGameState GetState<TState>()
        {
            if (_allStates == null) InitializeStates();
            return _allStates[typeof(TState)];
        }

        private void InitializeStates()
        {
            _allStates = new Dictionary<Type, IGameState>
            {
                [typeof(BootstrapState)] = _container.Instantiate<BootstrapState>(),
                [typeof(MainMenuState)] = _container.Instantiate<MainMenuState>(),
                [typeof(LoadLevelState)] = _container.Instantiate<LoadLevelState>(),
                [typeof(GameLoopState)] = _container.Instantiate<GameLoopState>(),
                [typeof(ProgressSavingState)] = _container.Instantiate<ProgressSavingState>()
            };
        }
    }
}