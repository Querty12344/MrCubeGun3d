using Infrastructure.Constants;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.Sound;
using UnityEngine;

namespace Infrastructure.SettingsManagement
{
    public class GameSettingsProvider : MonoBehaviour
    {
        public CameraSettings CameraSettings;
        public OptimizationSettings OptimizationSettings;
        public EntitySettings EntitySettings;
        public GenerationStaticData GenerationSettings;
        public SoundData SoundSettings;
    }
}