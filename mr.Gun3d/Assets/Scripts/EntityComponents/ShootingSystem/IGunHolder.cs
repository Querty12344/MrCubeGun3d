using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public interface IGunHolder
    {
        void StartAiming();
        void StopAiming();
        Transform GetGunTransform();
        void SetGun(Gun gun);
        void UpdateRotation(Vector3 direction);
        void Shoot(Vector3 direction);
        void ActivateAimLine();
        void DeactivateAimLine();
        float GetShootingTime();
        void ShootOnce(Vector3 direction);
    }
}