using Infrastructure.ResourceManagement.StaticData;

namespace Infrastructure.SettingsManagement
{
    public class GameDifficulty : IGameDifficulty
    {
        private readonly GameDifficultyStaticData _difficulty;

        public GameDifficulty(StaticData staticData)
        {
            _difficulty = staticData.GetGameDifficulty();
        }

        public float GetAimSpeed()
        {
            return _difficulty.AimSpeed;
        }

        public float GetAimMaxOffset()
        {
            return _difficulty.AimMaxOffset;
        }

        public float GetBaseAimOffset()
        {
            return _difficulty.AimBaseOffset;
        }
    }
}