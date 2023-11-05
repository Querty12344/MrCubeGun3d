using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/GameSettings", fileName = "GameDifficulty")]
    public class GameDifficultyStaticData : ScriptableObject
    {
        public float AimSpeed;
        public float AimSpeedByTimeCof;
        public float AimMaxOffset;
        public float AimBaseOffset;
    }
}