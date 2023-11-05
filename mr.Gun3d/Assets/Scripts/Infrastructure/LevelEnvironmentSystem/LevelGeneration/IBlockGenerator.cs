using EnvironmentComponents;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface IBlockGenerator
    {
        void GenerateBlock(Vector3 at,int style);
        LevelFloor GenerateBlockPlatform(Vector3 at, int style, int rotationCof, int length = 0, int width = 0,
            int height = 0);
        void ClearLevel();
        Vector3 GetLastRoadEndPos();
        void ActivateAutoBlockUpdate();
        void DeactivateAutoBlockUpdate();
        void UpdateBlockView();
    }
}