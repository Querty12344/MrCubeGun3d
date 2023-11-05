using Infrastructure.EntitiesManagement;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class ShootingState : IGameLoopState
    {
        private readonly ILevelEnemiesHolder _enemies;
        private readonly IGameLoopStateMachine _stateMachine;
        private bool _enemyDamaged;

        public ShootingState(ILevelEnemiesHolder enemies, IGameLoopStateMachine stateMachine)
        {
            _enemies = enemies;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _enemyDamaged = false;
            _enemies.ActiveEnemy.EnemyHealth.OnChanged += EnemyDamaged;
            _enemies.Player.Health.OnDead += EndGame;
            PlayerShoot();
        }

        private void EnemyShoot()
        {
            if (_enemies.ActiveEnemy != null && !_enemyDamaged)
                _enemies.ActiveEnemy.EnemyShooter.StartShooting(ShootingEnded);
            else
                _stateMachine.EnterState<LevelGenerationState>();
        }

        private void PlayerShoot()
        {
            _enemies.Player.OnStairsShooter.StartShooting(EnemyShoot);
        }

        private void EnemyDamaged(float x)
        {
            _enemyDamaged = true;
        }
        private void ShootingEnded()
        {
            if (_enemies.Player.Health.IsAlive)
            {
                _enemies.Player.Health.OnDead -= EndGame;
                _stateMachine.EnterState<LevelGenerationState>();
            }
        }

        private void EndGame()
        {
            _stateMachine.EnterState<LevelClearingState>();
            _stateMachine.EnterState<PlayerLoseState>();
        }
    }
}