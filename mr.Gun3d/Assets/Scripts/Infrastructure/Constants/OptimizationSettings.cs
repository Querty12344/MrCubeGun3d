using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.Constants
{
    [CreateAssetMenu(menuName = "", fileName = "Optimization")]
    public class OptimizationSettings : ScriptableObject
    {
        public float CoinLifetime = 0.5f;
        public float UIEffectsTime = 0.5f;
        public float BlockDisableDistance;
        public float BlockUpdateTiming;
        public float MaxYOffset;
        public float CoinSpawnOffset = 0.1f;
        public int MoneyReward;
    }
}