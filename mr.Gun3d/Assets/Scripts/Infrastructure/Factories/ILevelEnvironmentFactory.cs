using EnvironmentComponents;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ILevelEnvironmentFactory
    {
        void CreateCoin(Vector3 at, float coinLifeTime);
        Block CreateBlock(Vector3 at,int style);
        void CreateRunningEndTrigger(Vector3 at);
        Transform CreateEmpty(Vector3 at);
        Block CreateEnvironment(Vector3 ePosition,int style);
        Block CreateLightEnvironment(Vector3 ePosition,int style);
        Block CreateTrap(Vector3 arg1, int arg2);
        void CreateGuide(Transform at) ;
    }
}