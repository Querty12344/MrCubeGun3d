using EntityComponents.EntityFacades;
using EntityComponents.ShootingSystem;
using EnvironmentComponents;
using Infrastructure.EffectsManagement;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.MoneyManagement;
using Infrastructure.Random;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.ScoreSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.Sound;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IShooterFactory _shooterFactory;
        private readonly IGameEffects _gameEffects;
        private readonly IInputService _input;
        private readonly IScore _score;
        private readonly IMoney _money;
        private readonly GameSettingsProvider _settingsProvider;
        private int _moneyAward;
        private ISound _sound;

        public EntityFactory(IAssetProvider assetProvider,
            ICoroutineRunner coroutineRunner,
            IShooterFactory shooterFactory,
            IScore score,
            GameSettingsProvider settingsProvider,
            IGameEffects gameEffects,
            IInputService input,
            IMoney money, ISound sound)
        {
            _coroutineRunner = coroutineRunner;
            _shooterFactory = shooterFactory;
            _score = score;
            _settingsProvider = settingsProvider;
            _gameEffects = gameEffects;
            _input = input;
            _money = money;
            _sound = sound;
            _assetProvider = assetProvider;
        }

        public EnemyFacade CreateRandomEnemy(LevelFloor floor)
        {
            EnemyStaticData enemyData = _assetProvider.GetRandomEnemy();
            var enemyGameObject =
                Object.Instantiate(enemyData.EnemyPrefab, floor.GetSpawnPosition(), Quaternion.identity);
            var enemy = enemyGameObject.GetComponent<EnemyFacade>();
            enemy.Construct(enemyData.GunTypeId);
            enemy.EnemyShooter = _shooterFactory.CreateAutoAimShooter(enemy.GunHolder);
            enemy.Mover.Construct(_coroutineRunner, floor, _settingsProvider, enemy.Animator,_sound);
            enemy.EnemyHealth.Construct(enemy.Mover,enemyData.Health,enemyData.InRunHealthModifer, enemyData.CriticalDamageModifier,enemyData.AwardChance, _gameEffects);
            _moneyAward = Randomizer.Range(enemyData.minMoneyAward, enemyData.MaxMoneyAward);
            enemy.Award.Construct(enemy.EnemyHealth, _gameEffects, _money,_score,_moneyAward, enemyData.SmallMoneyAward,enemyData.ScoreAward);
            return enemy;
        }

        public PlayerFacade InstantiatePlayer(LevelFloor floor)
        {
            var playerStaticData = _assetProvider.GetPlayer();
            var playerObject =
                Object.Instantiate(playerStaticData.Prefab, floor.GetSpawnPosition(), Quaternion.identity);
            var player = playerObject.GetComponent<PlayerFacade>();
            player.Construct();
            player.InRunShooter = _shooterFactory.CreateInRunShooter(player.GunHolder);
            player.OnStairsShooter = _shooterFactory.CreateOnStairsShooter(player.GunHolder);
            player.Mover.Construct(_coroutineRunner, floor, _settingsProvider, player.Animator,_sound);
            player.Mover.ConnectInput(_input);
            player.Health.Construct(playerStaticData.Health, _gameEffects);
            return player;
        }
        
    }
}