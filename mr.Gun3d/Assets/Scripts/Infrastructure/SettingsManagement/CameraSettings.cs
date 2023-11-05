using UnityEngine;

namespace Infrastructure.SettingsManagement
{
    [CreateAssetMenu(menuName = "GameSettings/Camera", fileName = "Camera")]
    public class CameraSettings : ScriptableObject

    {
        public float XOffset;
        public float Speed = 10f;
        public float YOffset = 6f;
        public float ZOffsetCof = 0.6f;
        public Vector3 Rotation;
        public float RotationSpeed;
        public float MobileZOffset;
        public float PcZOffset;
        public float UltraWidthZOffset;
    }
}