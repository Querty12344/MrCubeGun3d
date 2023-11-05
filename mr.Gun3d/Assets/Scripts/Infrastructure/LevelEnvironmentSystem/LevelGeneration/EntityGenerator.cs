using EntityComponents.EntityFacades;
using EntityComponents.ShootingSystem;
using EnvironmentComponents;
using Infrastructure.DataSavingSystem;
using Infrastructure.Factories;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using Infrastructure.UI.UIInfrostructure;
using TMPro;
using UnityEditor;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class EntityGenerator : IEntityGenerator
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IGunFactory _gunFactory;
        private readonly IPlayerConfig _playerConfig;
        private readonly IUIService _uiService;

        public EntityGenerator(IEntityFactory entityFactory, IGunFactory gunFactory, IPlayerConfig playerConfig,
            IUIService uiService)
        {
            _entityFactory = entityFactory;
            _gunFactory = gunFactory;
            _playerConfig = playerConfig;
            _uiService = uiService;
        }

        public EnemyFacade GenerateEnemy(LevelFloor floor)
        {
            EnemyFacade enemy = _entityFactory.CreateRandomEnemy(floor);
            Gun gun = _gunFactory.CreateGun(enemy.GunHolder, enemy.GunType);
            enemy.GunHolder.SetGun(gun);
            _uiService.ActivateHPBar(enemy.EnemyHealth,true);
            return enemy;
        }
        
        public PlayerFacade GeneratePlayer(LevelFloor floor)
        {
            var player = _entityFactory.InstantiatePlayer(floor);
            if (_playerConfig.ActiveGunId == 0)
            {
                _playerConfig.ActiveGunId = GunTypeId.A1;
            }
            var gun = _gunFactory.CreateGun(player.GunHolder, _playerConfig.ActiveGunId);
            player.GunHolder.SetGun(gun);
            _uiService.ActivateHPBar(player.Health,false);
            return player;
        }
    }
}