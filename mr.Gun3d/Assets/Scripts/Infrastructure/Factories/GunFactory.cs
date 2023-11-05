using System;
using EntityComponents.ShootingSystem;
using Infrastructure.GameCore;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.Sound;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factories
{
    public class GunFactory : IGunFactory
    {
        private readonly IGunAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private ISound _sound;


        public GunFactory(IGunAssetProvider assetProvider, ICoroutineRunner coroutineRunner, ISound sound)
        {
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
            _sound = sound;
        }

        public Gun CreateGun(IGunHolder gunHolder, GunTypeId gunType)
        {
            var gunData = _assetProvider.GetGun(gunType);
            var gunObject = Object.Instantiate(gunData.Prefab, gunHolder.GetGunTransform());
            var gun = gunObject.GetComponent<Gun>();
            gun.Construct(gunData, this, _coroutineRunner,_sound);
            return gun;
        }

        public void CreateBullet(BulletStaticData bulletData,float damage, Transform at, Vector3 direction)
        {
            var bullet = Object.Instantiate(bulletData.Prefab, at).GetComponent<Bullet>();
            bullet.Construct(direction,damage, bulletData);
        }

        public GameObject CreateAimLine(GameObject aimLine, Transform at)
        {
            return Object.Instantiate(aimLine, at);
        }
    }
}