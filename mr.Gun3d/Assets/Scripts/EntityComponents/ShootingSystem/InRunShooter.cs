using System;
using System.Collections;
using System.Collections.Generic;
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
    public class InRunShooter:ShooterBase,IShooter
    {
        private readonly IGunFactory _gunFactory;
        private ICoroutineRunner _coroutineRunner;
        private IGunHolder _gunHolder;
        private readonly ILevelEnemiesHolder _enemies;
        private IInputService _input;
        private Action _shootingEnded;
        private float _shootingStartTime;
        private bool _inRunShooting;
        private bool _wantShootInRun;

        public InRunShooter(IGunHolder gunHolder,ILevelEnemiesHolder enemies, IInputService input, ICoroutineRunner coroutineRunner)
        {
            _gunHolder = gunHolder;
            _enemies = enemies;
            _coroutineRunner = coroutineRunner;
            _input = input;
            _inRunShooting = false;
        }
        public void StartShooting(Action shootingEnded = null)
        {
            _shootingEnded = shootingEnded;
            _inRunShooting = true;
            _input.ActivateShooting(InRunShoot);
            _coroutineRunner.StartCoroutine(InRunShooting());
        }

        public void EndShooting()
        {
            _inRunShooting = false;
            _input.DeactivateShooting();
            _shootingEnded?.Invoke();
        }
        private void InRunShoot()
        {
            _wantShootInRun = true;
        }
        private IEnumerator InRunShooting()
        {
            _gunHolder.StartAiming();
            while (_inRunShooting)
            {
                if (_wantShootInRun && _enemies.Player != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo) )
                    {
                        Vector3 direction = (-_gunHolder.GetGunTransform().position + hitInfo.point);
                        direction.z = 0f;
                        Vector3 toPlayerDirection = _gunHolder.GetGunTransform().position - _enemies.Player.transform.position;
                        toPlayerDirection.Normalize();
                        if (toPlayerDirection.x > 0 && direction.x > 0 || toPlayerDirection.x < 0 && direction.x < 0)
                        {
                            _gunHolder.UpdateRotation(direction);
                            _gunHolder.ShootOnce(direction);
                            yield return new WaitForSeconds(_gunHolder.GetShootingTime());
                            _wantShootInRun = false;   
                        }
                        _wantShootInRun = false;   
                    } 
                }
                yield return null;
            }
        }
    }
}