using System.Collections;
using Infrastructure.EntitiesManagement;
using UnityEngine;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class EnitiesMovingState : IGameLoopState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILevelEnemiesHolder _enemies;
        private readonly IGameLoopStateMachine _gameLoopStateMachine;

        public EnitiesMovingState(ICoroutineRunner coroutineRunner, IGameLoopStateMachine gameLoopStateMachine,
            ILevelEnemiesHolder enemies)
        {
            _coroutineRunner = coroutineRunner;
            _gameLoopStateMachine = gameLoopStateMachine;
            _enemies = enemies;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            _enemies.ActiveEnemy.Mover.ChangePosition();
            _enemies.Player.Mover.ChangePosition();
            yield return new WaitUntil(() => !_enemies.ActiveEnemy.Mover.IsMoving);
            yield return new WaitUntil(() => !_enemies.Player.Mover.IsMoving);
            _enemies.ActiveEnemy.Mover.RotateToTarget(_enemies.Player.transform.position);
            _enemies.Player.Mover.RotateToTarget(_enemies.ActiveEnemy.transform.position);
            yield return new WaitForSeconds(0.3f);
            _gameLoopStateMachine.EnterState<ShootingState>();
        }
    }
}