using System.Collections;
using System.Collections.Generic;
using EnvironmentComponents;
using Infrastructure.EntitiesManagement;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.GameCore.GameLoopStates
{
    public class RunningState:IGameLoopState
    {
        private readonly ILevelGenerator _levelGenerator;
        private readonly ILevelEnemiesHolder _enemies;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameLoopStateMachine _stateMachine;

        public RunningState(ILevelGenerator levelGenerator,ILevelEnemiesHolder enemies,ICoroutineRunner coroutineRunner,
            IGameLoopStateMachine stateMachine)
        {
            _levelGenerator = levelGenerator;
            _enemies = enemies;
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(Run());
            
        }

        private IEnumerator Run()
        {
            _enemies.ActiveEnemy.Mover.ChangePosition();
            _enemies.Player.Mover.ChangePosition();
            yield return new WaitUntil(() => !_enemies.Player.Mover.IsMoving);
            _enemies.Player.Mover.RotateToTarget(_enemies.ActiveEnemy.transform.position);
            int rotationCof = _levelGenerator.GenerateRunningWay();
            _enemies.ActiveEnemy.Mover.StartRunning(rotationCof,_enemies.Player.transform,false);
            LevelFloor lastFloor = _enemies.ActiveEnemy.Mover.ActiveFloor;
            _enemies.Player.Mover.SetNextFloor(lastFloor);
            _enemies.Player.Mover.ChangePosition();
            yield return new WaitUntil(() => !_enemies.Player.Mover.IsMoving);
            _enemies.Player.Mover.StartRunning(rotationCof, _enemies.ActiveEnemy.transform,true);
            _enemies.Player.InRunShooter.StartShooting();
            while (_enemies.Player.Mover.IsRunning)
            {
                if (_enemies.ActiveEnemy != null)
                {
                    if (!_enemies.ActiveEnemy.Mover.IsRunning)
                    {
                        _enemies.ActiveEnemy.RunAwayFromLevel();
                        _enemies.Player.Mover.StopRunning();
                        _enemies.Player.InRunShooter.EndShooting();
                        break;
                    }
                }
                if (_enemies.ActiveEnemy == null)
                {
                    _enemies.Player.Mover.StopRunning();
                    _enemies.Player.InRunShooter.EndShooting(); 
                    break;
                }
                yield return null;
            }
            _enemies.Player.InRunShooter.EndShooting();
            _stateMachine.EnterState<LevelGenerationState>();
        }
    }
}