using EntityComponents.ShootingSystem;
using Infrastructure.EntitiesManagement;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.SettingsManagement;

namespace Infrastructure.Factories
{
    public class ShooterFactory : IShooterFactory
    {
        private readonly ILevelEnemiesHolder _enemies;
        private readonly IGameDifficulty _difficulty;
        private readonly IInputService _input;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameSettingsProvider _settingsProvider;

        public ShooterFactory(ILevelEnemiesHolder enemies,IGameDifficulty difficulty, IInputService input, 
            ICoroutineRunner coroutineRunner, GameSettingsProvider settingsProvider)
        {
            _enemies = enemies;
            _difficulty = difficulty;
            _input = input;
            _coroutineRunner = coroutineRunner;
            _settingsProvider = settingsProvider;
        }

        public OnStairsShooter CreateOnStairsShooter(IGunHolder gunHolder)
        {
            return new OnStairsShooter(gunHolder,_enemies,_difficulty,_input,_coroutineRunner,_settingsProvider);
        }

        public AutoAimShooter CreateAutoAimShooter(IGunHolder gunHolder)
        {
            return new AutoAimShooter(gunHolder,_coroutineRunner,_settingsProvider,_enemies);
        }

        public InRunShooter CreateInRunShooter(IGunHolder gunHolder)
        {
            return new InRunShooter(gunHolder, _enemies,_input,_coroutineRunner);
        }
    }
}