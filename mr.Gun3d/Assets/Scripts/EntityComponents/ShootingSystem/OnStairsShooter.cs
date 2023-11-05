using System;
using System.Collections;
using EntityComponents.Health;
using Infrastructure.Constants;
using Infrastructure.EntitiesManagement;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public class OnStairsShooter : ShooterBase, IShooter
    {
        private readonly IGunFactory _gunFactory;
        private ICoroutineRunner _coroutineRunner;
        private IGameDifficulty _difficulty;
        private IGunHolder _gunHolder;
        private IInputService _input;
        private bool _isAiming;
        private Vector3 _rightDirection;
        private EntitySettings _settings;
        private Action _shootingEnded;
        private float _shootingStartTime;
        private ILevelEnemiesHolder _enemies;

        public OnStairsShooter(IGunHolder gunHolder,ILevelEnemiesHolder enemies,IGameDifficulty difficulty, IInputService input, ICoroutineRunner coroutineRunner,
            GameSettingsProvider settingsProvider)
        {
            _enemies = enemies;
            _difficulty = difficulty;
            _settings = settingsProvider.EntitySettings;
            _gunHolder = gunHolder;
            _coroutineRunner = coroutineRunner;
            _input = input;
        }

        public void StartShooting(Action shootingEnded)
        {
            _shootingEnded = shootingEnded;
            _isAiming = true;
            _input.ActivateShooting(Shoot);
            _coroutineRunner.StartCoroutine(Aiming(_enemies.Player.transform, _enemies.ActiveEnemy.transform));
        }

        public void EndShooting()
        {
            _isAiming = false;
            _input.DeactivateShooting();
            _gunHolder.DeactivateAimLine();
        }


        private IEnumerator Aiming(Transform shooter, Transform target)
        {
            _gunHolder.StartAiming();
            _rightDirection = CalculateRightDirection(shooter, target, _difficulty.GetBaseAimOffset());
            _gunHolder.ActivateAimLine();
            _shootingStartTime = Time.time;
            while (_isAiming)
            {
                _gunHolder.UpdateRotation(CalculateShootingDirection());
                yield return null;
            }
            
        }


        private Vector3 CalculateShootingDirection(float timeOffset = 0f)
        {
            var offsetDirectionY =
                (float)Math.Sin((Time.time - _shootingStartTime - timeOffset) * _difficulty.GetAimSpeed());
            var offset = new Vector3(0, offsetDirectionY, 0) * _difficulty.GetAimMaxOffset();
            var shootingDirection = _rightDirection + offset;
            return shootingDirection.normalized;
        }

        private void Shoot()
        {
            EndShooting();
            _gunHolder.Shoot(CalculateShootingDirection());
            _coroutineRunner.StartCoroutine(Shooting());
        }
        
        
        private IEnumerator Shooting()
        {
            yield return new WaitForSeconds(_settings.ShootingTime);
            _shootingEnded?.Invoke();
            if (_isAiming == false)
            {
                _gunHolder.StopAiming();
            }
        }
    }
}