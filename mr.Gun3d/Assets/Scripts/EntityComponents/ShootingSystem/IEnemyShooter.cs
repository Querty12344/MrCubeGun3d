using System;
using Infrastructure.GameCore;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public interface IEnemyShooter
    {
        void Construct(IGunHolder gunHolder, ICoroutineRunner coroutineRunner, GameSettingsProvider settingsProvider);
        void Shoot(Transform target, Action shootingEnded);
    }
}