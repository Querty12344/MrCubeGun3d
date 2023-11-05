using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.Constants
{
    [CreateAssetMenu(menuName = "GameSettings", fileName = "entity")]
    public class EntitySettings : ScriptableObject
    {
        public float OnPlaceDistance = 1f;
        public float EntitiesMovingSpeed = 0.25f;
        public float ShootingTime = 0.8f;
        public float RotationSpeed = 0.01f;
        public float JumpForce;
        public float RunSpeed;
        public float JumpOffset;
        public float ToEnemyDistance;
        public float StartJumpOffset;
    }
}