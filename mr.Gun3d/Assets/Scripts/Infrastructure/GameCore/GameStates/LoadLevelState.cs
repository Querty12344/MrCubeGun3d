using Infrastructure.Constants;
using Infrastructure.SceneManagment;
using Infrastructure.UI.UIElements;
using UnityEngine;

namespace Infrastructure.GameCore.GameStates
{
    public class LoadLevelState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;

        public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load("Game", OnGameSceneLoaded);
        }
        

        public void OnGameSceneLoaded()
        {
            _stateMachine.EnterState<GameLoopState>();
        }
    }
}