using EnvironmentComponents;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface IFloorGenerator
    {
        int LevelStyleIndex { get; }
        LevelFloor GenerateNextFloor();
        void ClearLevel();
        void ChooseLevelStyle();
        void SetGenerationStartPosition(Vector3 at);
    }
}