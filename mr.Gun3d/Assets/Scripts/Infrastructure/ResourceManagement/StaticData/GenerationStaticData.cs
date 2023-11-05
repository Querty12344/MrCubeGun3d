using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(fileName = "FloorGenerationSettings", menuName = "StaticData/GenerationSettings")]
    public class GenerationStaticData : ScriptableObject
    {
        public float MaxYOffset;
        public float MinYOffset;
        public float MaxXOffset;
        public float MinXOffset;
        public int UnderBossEnemyCount;
        public float BlockOffset;
        public Vector3 EnemyOnOffset;
        public int DefaultFloorLength;
        public int DefaultFloorWidth;
        public int DefaultFloorHeight;
        public int AvailableBlockCount;
        public int roadHeight;
        public int roadWidth;
        public int roadLength;
        public int RoadСurvature;
        public int basicStatesMaxCount;
        public int EnvChance;
        public int LightEnvChance;
        public int[] GreenStyles;
        public int[] GreenEnvStyles;
        public int[] GreenLEnvStyles;
        public int TrapChance;
    }
}