using EnvironmentComponents;
using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BulletData", fileName = "BulletData")]
    public class BulletStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public Effect StartEffect;
        public Effect ConstantEffect;
        public Effect CollisionEffect;
        public float Speed;
        public float BulletLifeTime;
    }
}