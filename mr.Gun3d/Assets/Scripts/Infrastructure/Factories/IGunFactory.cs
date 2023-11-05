using EntityComponents.ShootingSystem;
using Infrastructure.ResourceManagement.StaticData;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGunFactory
    {
        Gun CreateGun(IGunHolder gunHolder, GunTypeId gunType);
        public void CreateBullet(BulletStaticData bulletData,float damage, Transform at, Vector3 direction);
        public GameObject CreateAimLine(GameObject aimLine, Transform at);
    }
}