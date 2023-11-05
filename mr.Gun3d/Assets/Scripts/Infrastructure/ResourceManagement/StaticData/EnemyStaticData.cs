using EntityComponents.ShootingSystem;
using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/EnemyData")]
    public class EnemyStaticData : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float Health;
        public float CriticalDamageModifier;
        public int minMoneyAward;
        public int MaxMoneyAward;
        public float InRunHealthModifer;
        public int SmallMoneyAward;
        public int ScoreAward;
        public GunTypeId GunTypeId;
        public int AwardChance = 1;
    }
}