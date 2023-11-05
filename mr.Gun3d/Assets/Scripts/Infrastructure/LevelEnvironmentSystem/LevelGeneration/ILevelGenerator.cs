using System;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface ILevelGenerator
    {
        void CreateLevelStart();
        void UpdateLevel(Action callback);
        void ClearLevel();
        int GenerateRunningWay();
    }
}