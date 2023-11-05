using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface IRunRoadGenerator
    {
        Vector3 GenerateStandardRoad(Vector3 start,int rotationCof,int style);
        void GenerateRoadFinish(Vector3 end);
    }
}