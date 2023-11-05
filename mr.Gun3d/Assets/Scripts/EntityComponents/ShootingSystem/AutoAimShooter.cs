using System;
using System.Collections;
using EntityComponents.Health;
using Infrastructure.Constants;
using Infrastructure.EntitiesManagement;
using Infrastructure.GameCore;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public class AutoAimShooter : ShooterBase, IShooter
    {
        private ICoroutineRunner _coroutineRunner;
        private IGunHolder _gunHolder;
        private EntitySettings _settings;
        private ILevelEnemiesHolder _enemies;

        public AutoAimShooter(IGunHolder gunHolder, ICoroutineRunner coroutineRunner,
            GameSettingsProvider settingsProvider,ILevelEnemiesHolder enemies)
        {
            _enemies = enemies;
            _settings = settingsProvider.EntitySettings;
            _gunHolder = gunHolder;
            _coroutineRunner = coroutineRunner;
            _gunHolder.UpdateRotation((enemies.Player.Mover.ActiveFloor.GetSpawnPosition() - _gunHolder.GetGunTransform().position).normalized+ Vector3.up*2);
        }

        public void StartShooting(Action shootingEnded)
        {
            _gunHolder.StartAiming();
            var shootDirection = CalculateRightDirection(_gunHolder.GetGunTransform(),_enemies.Player.transform , 0.7f);
            _gunHolder.UpdateRotation(shootDirection);
            _gunHolder.Shoot(shootDirection);
            _coroutineRunner.StartCoroutine(Shooting(shootingEnded));
        }
        private IEnumerator Shooting(Action shootingEnded)
        {
            yield return new WaitForSeconds(_settings.ShootingTime);
            shootingEnded?.Invoke();
            _gunHolder.StopAiming();
        }
        
    }
}