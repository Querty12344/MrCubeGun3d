using System.Collections;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.Random;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.Sound;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPos;
        private ICoroutineRunner _coroutineRunner;
        private IGunFactory _gunFactory;
        private GunStaticData _gunSettings;
        private ISound _sound;

        public void Construct(GunStaticData gunSettings, IGunFactory gunFactory, ICoroutineRunner coroutineRunner,ISound sound)
        {
            _sound = sound;
            _coroutineRunner = coroutineRunner;
            _gunFactory = gunFactory;
            _gunSettings = gunSettings;
        }

        public GameObject GetAimLine()
        {
            return _gunFactory.CreateAimLine(_gunSettings.AimLine, _bulletSpawnPos);
        }

        public virtual void Shoot(Vector3 direction)
        {
            _coroutineRunner.StartCoroutine(Shooting(direction));
        }

        private IEnumerator Shooting(Vector3 direction)
        {
            for (var i = 0; i < _gunSettings.BulletsInShoot; i++)
            {
                var finalDirection = AddSpread(direction);
                _sound.PlayShootSound(_gunSettings.VolumeTone);
                _gunFactory.CreateBullet(_gunSettings.Bullet,_gunSettings.Damage, _bulletSpawnPos, finalDirection);
                yield return new WaitForSeconds(_gunSettings.ShootingDuratuion / _gunSettings.BulletsInShoot);
            }
        }

        public void OneShoot(Vector3 direction)
        {
            _sound.PlayShootSound(_gunSettings.VolumeTone);
            _gunFactory.CreateBullet(_gunSettings.Bullet,_gunSettings.Damage, _bulletSpawnPos, direction);
        }
        private Vector3 AddSpread(Vector3 direction)
        {
            var spreadDirection = new Vector3(0, Randomizer.Range(-_gunSettings.Spread, _gunSettings.Spread), 0);
            return (direction + spreadDirection).normalized;
        }

        public float GetShootingTime()
        {
            return _gunSettings.InRunShootingTiming;
        }
    }
}