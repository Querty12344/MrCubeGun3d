using System;
using EntityComponents.ShootingSystem;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace EntityComponents.Health
{
    public interface IShooter
    {
        void StartShooting(Action shootingEnded = null);
        void EndShooting(){}
    }
}