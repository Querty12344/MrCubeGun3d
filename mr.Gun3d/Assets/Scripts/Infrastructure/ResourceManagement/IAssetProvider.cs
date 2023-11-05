using EnvironmentComponents;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.ResourceManagement.StaticData;
using UnityEngine;

namespace Infrastructure.ResourceManagement
{
    public interface IAssetProvider
    {
        EnemyStaticData GetRandomEnemy();
        PlayerStaticData GetPlayer();
        GameObject GetCoin();
        Block GetBlock(int style);
        RunningEndTrigger GetRunningEnd();
        Transform GetEmpty();
        Block GetEnvironmentBlock(int style);
        Block GetLightEnvironmentBlock(int style);
        Block GetTrap();
        GameObject GetGuide();
    }
}