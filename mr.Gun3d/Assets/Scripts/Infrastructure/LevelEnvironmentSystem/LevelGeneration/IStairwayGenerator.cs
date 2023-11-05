using System;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface IStairwayGenerator
    {   
        void GenerateStairway(int styleIndex, Vector3 start, Vector3 end);
    }
}