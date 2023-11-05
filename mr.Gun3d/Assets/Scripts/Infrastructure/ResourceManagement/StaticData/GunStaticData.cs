using EntityComponents.ShootingSystem;
using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/GunData", fileName = "GunData")]
    public class GunStaticData : ScriptableObject
    {
        public GunTypeId GunType;
        public GameObject Prefab;
        public GameObject AimLine;
        public BulletStaticData Bullet;
        public int BulletsInShoot;
        public float ShootingDuratuion;
        public float Spread;
        public int Price;
        public float InRunShootingTiming = 0.5f;
        public string Name;
        public float Damage;
        public float VolumeTone = 1;
    }
}