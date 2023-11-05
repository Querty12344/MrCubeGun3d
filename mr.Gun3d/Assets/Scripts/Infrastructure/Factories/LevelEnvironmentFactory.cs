using EnvironmentComponents;
using Infrastructure.GameCore;
using Infrastructure.ResourceManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class LevelEnvironmentFactory : ILevelEnvironmentFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private float _timeOffset;

        public LevelEnvironmentFactory(IAssetProvider assetProvider,ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
        }
        public void CreateCoin(Vector3 at, float coinLifeTime)
        {
            var coinPrefab = _assetProvider.GetCoin();
            var coin = Object.Instantiate(coinPrefab, at, Quaternion.identity).GetComponent<Coin>();
            coin.Construct(coinLifeTime);
        }

        public Block CreateBlock(Vector3 at, int style)
        {
            Block prefab = _assetProvider.GetBlock(style);
            Block next = Object.Instantiate(prefab,at,Quaternion.identity);
            next.Init(_coroutineRunner);
            return next;
        }

        public void CreateRunningEndTrigger(Vector3 at)
        {
            RunningEndTrigger prefab = _assetProvider.GetRunningEnd();
            Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public Transform CreateEmpty(Vector3 at)
        {
            Transform prefab = _assetProvider.GetEmpty();
            Transform next = Object.Instantiate(prefab,at,Quaternion.identity).transform;
            return next;
        }

        public Block CreateEnvironment(Vector3 ePosition,int style)
        {
            Block prefab = _assetProvider.GetEnvironmentBlock(style);
            Block next = Object.Instantiate(prefab,ePosition,Quaternion.identity);
            next.Init(_coroutineRunner);
            return next;
        }

        public Block CreateLightEnvironment(Vector3 ePosition,int style)
        {
            Block prefab = _assetProvider.GetLightEnvironmentBlock(style);
            Block next = Object.Instantiate(prefab,ePosition,Quaternion.identity);
            next.Init(_coroutineRunner);
            return next;
        }

        public Block CreateTrap(Vector3 at, int style)
        {
            Block prefab = _assetProvider.GetTrap();
            Block next = Object.Instantiate(prefab,at,Quaternion.identity);
            next.Init(_coroutineRunner);
            return next;
        }

        public void CreateGuide(Transform at)
        {
            GameObject.Instantiate(_assetProvider.GetGuide(),at.position,Quaternion.identity);
        }
    }
}