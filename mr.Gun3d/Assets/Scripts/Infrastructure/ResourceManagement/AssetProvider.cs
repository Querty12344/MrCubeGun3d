using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.ResourceManagement.StaticData;
using UnityEngine;

namespace Infrastructure.ResourceManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly StaticData.StaticData _staticData;

        public AssetProvider(StaticData.StaticData staticData)
        {
            _staticData = staticData;
        }
        
        public EnemyStaticData GetRandomEnemy()
        {
            return _staticData.GetRandomEnemy();
        }

        public PlayerStaticData GetPlayer()
        {
            return _staticData.GetPlayer();
        }
        
        public GameObject GetCoin()
        {
            return Resources.Load<GameObject>(ResourcesPath.Coin);
        }
        
        public Block GetBlock(int style)
        {
            return _staticData.GetBlock(style);
        }

        public RunningEndTrigger GetRunningEnd()
        {
            return _staticData.GetRunningEndTrigger();
        }

        public Transform GetEmpty()
        {
            return _staticData.GetEmpty();
        }

        public Block GetEnvironmentBlock(int style)
        {
            return _staticData.GetRandomEnvironment(style);
        }

        public Block GetLightEnvironmentBlock(int style)
        {
            return _staticData.GetRandomLightEnvironment(style);
        }

        public Block GetTrap()
        {
            return _staticData.GetTrap();
        }

        public GameObject GetGuide()
        {
            return _staticData.GetGuide();
        }
    }
}