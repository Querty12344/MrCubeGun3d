namespace Infrastructure.SettingsManagement
{
    public interface IGameDifficulty
    {
        float GetAimSpeed();
        float GetAimMaxOffset();
        float GetBaseAimOffset();
    }
}