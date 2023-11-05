using System.Collections;
using CameraComponents;
using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.SettingsManagement;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.EffectsManagement
{
    public class GameEffects : IGameEffects
    {
        private readonly ICameraEffects _cameraEffects;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILevelEnvironmentFactory _environmentFactory;
        private readonly OptimizationSettings _settings;
        private readonly IUIService _uiService;

        public GameEffects(IUIService uiService,
            ILevelEnvironmentFactory environmentFactory,
            ICoroutineRunner coroutineRunner,
            ICameraEffects cameraEffects,
            GameSettingsProvider settingsProvider)
        {
            _uiService = uiService;
            _environmentFactory = environmentFactory;
            _coroutineRunner = coroutineRunner;
            _cameraEffects = cameraEffects;
            _settings = settingsProvider.OptimizationSettings;
        }

        public void PlayCriticalDamageEffect()
        {
            _cameraEffects.StrongShakeCamera();
            _uiService.ShowCriticalDamageEffect();
        }

        public void PlayDamageEffect()
        {
            _cameraEffects.LiteShakeCamera();
            _uiService.ShowDamageEffect();
        }

        public void PlayCoinEffect(Vector3 coinSpawnPosition, int moneyCount)
        {
            _coroutineRunner.StartCoroutine(CreateCoins(coinSpawnPosition, moneyCount));
        }

        private IEnumerator CreateCoins(Vector3 at, int count)
        {
            for (var i = 0; i < count; i++)
            {
                _environmentFactory.CreateCoin(at, _settings.CoinLifetime);
                yield return new WaitForSeconds(_settings.CoinSpawnOffset);
            }
        }
    }
}