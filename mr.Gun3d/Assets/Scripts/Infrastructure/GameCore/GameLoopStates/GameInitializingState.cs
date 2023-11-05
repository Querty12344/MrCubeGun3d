using CameraComponents;
using Infrastructure.Factories;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.ResourceManagement;
using Infrastructure.ScoreSystem;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using YG;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class GameInitializingState : IGameLoopState
    {
        private readonly ICameraFollow _camera;
        private readonly IGameLoopStateMachine _gameLoopStateMachine;
        private readonly ILevelEnvironmentFactory _levelEnvironmentFactory;
        private readonly IGunAssetProvider _gunAsset;
        private readonly ILevelGenerator _levelGenerator;
        private readonly IUIService _ui;
        private IScore _score;


        public GameInitializingState(IGameLoopStateMachine gameLoopStateMachine,ILevelEnvironmentFactory levelEnvironmentFactory,
            IGunAssetProvider gunAsset,
            ILevelGenerator levelGenerator,
            ICameraFollow camera,
            IUIService ui, IScore score)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
            _levelEnvironmentFactory = levelEnvironmentFactory;
            _gunAsset = gunAsset;
            _levelGenerator = levelGenerator;
            _camera = camera;
            _ui = ui;
            _score = score;
        }

        public void Enter()
        {
            Debug.Log("ScoreCleared");
            _score.ClearScore();
            _ui.EnableGameLoopUI();
            _levelGenerator.CreateLevelStart();
            _camera.Activate();
            _gameLoopStateMachine.EnterState<EnitiesMovingState>();
        }
    }
}