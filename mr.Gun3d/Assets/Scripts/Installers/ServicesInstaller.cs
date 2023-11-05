using CameraComponents;
using EntityComponents.ShootingSystem;
using EnvironmentComponents;
using Infrastructure.Adds;
using Infrastructure.DataSavingSystem;
using Infrastructure.EffectsManagement;
using Infrastructure.EntitiesManagement;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.GunShop;
using Infrastructure.InputSystem;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.MoneyManagement;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SceneManagment;
using Infrastructure.ScoreSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.Sound;
using Infrastructure.UI.UIElements;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        private GameBootstrap _bootstrap;
        private LevelStartPosition _startPosition;
        private InputService _inputService;
        private GameSettingsProvider _gameSettings;

        public override void InstallBindings()
        {
            InstallSceneServices();
            InstallResourceManagement();
            InstallGameLogic();
            InstallFactories();
            InstallLevelGenerators();
        }

        private void InstallSceneServices()
        {
            _bootstrap = FindObjectOfType<GameBootstrap>();
            _startPosition = FindObjectOfType<LevelStartPosition>();
            _inputService = FindObjectOfType<InputService>();
            _gameSettings = FindObjectOfType<GameSettingsProvider>();
            Container.Bind<GameBootstrap>().FromInstance(_bootstrap).AsSingle().NonLazy();
        }
        

        private void InstallFactories()
        {
            Container.Bind<IShooterFactory>().To<ShooterFactory>().AsSingle();
            Container.Bind<IBlockGenerator>().To<BlockGenerator>().AsSingle();
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
            Container.Bind<ILevelEnvironmentFactory>().To<LevelEnvironmentFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IGunFactory>().To<GunFactory>().AsSingle();
        }

        private void InstallGameLogic()
        {
            Container.Bind<ISound>().To<Sound>().AsSingle();
            Container.Bind<IGameEffects>().To<GameEffects>().AsSingle();
            Container.Bind<IScore>().To<Score>().AsSingle();
            Container.Bind<GameSettingsProvider>().FromInstance(_gameSettings).AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(_bootstrap).AsSingle();
            Container.Bind<IGameStatesHandler>().To<GameStatesHandler>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IUIService>().To<UIService>().AsSingle();
            Container.Bind<IGame>().To<Game>().AsSingle();
            Container.Bind<IGameLoopStateMachine>().To<GameLoopStateMachine>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ILoadingCurtain>().To<LoadingCurtain>().AsSingle();
            Container.Bind<LevelStartPosition>().FromInstance(_startPosition).AsSingle();
            Container.Bind<ILevelEnemiesHolder>().To<LevelEnemiesHolder>().AsSingle();
            Container.Bind<IInputService>().FromInstance(_inputService).AsSingle();
            Container.Bind<ICameraFollow>().To<CameraFollow>().AsSingle();
            Container.Bind<ICameraEffects>().To<CameraEffects>().AsSingle();
            Container.Bind<IMoney>().To<Money>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IPlayerConfig>().To<PlayerConfig>().AsSingle();
            Container.Bind<IGunShop>().To<GunShop>().AsSingle();
            Container.Bind<IAddsService>().To<AddsService>().AsSingle();
        }

        private void InstallLevelGenerators()
        {
            Container.Bind<IRunRoadGenerator>().To<RunRoadGenerator>().AsSingle();
            Container.Bind<ILevelGenerator>().To<LevelGenerator>().AsSingle();
            Container.Bind<IFloorGenerator>().To<FloorGenerator>().AsSingle();
            Container.Bind<IEntityGenerator>().To<EntityGenerator>().AsSingle();
            Container.Bind<IStairwayGenerator>().To<StairwayGenerator>().AsSingle();
        }

        private void InstallResourceManagement()
        {
            Container.Bind<StaticData>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IUIAssetProvider>().To<UIAssetProvider>().AsSingle();
            Container.Bind<IGunAssetProvider>().To<GunAssetProvider>().AsSingle();
            Container.Bind<IGameDifficulty>().To<GameDifficulty>().AsSingle();
        }
    }
}