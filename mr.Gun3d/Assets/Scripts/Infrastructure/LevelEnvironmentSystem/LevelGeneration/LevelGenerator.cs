using System;
using EnvironmentComponents;
using Infrastructure.EntitiesManagement;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class LevelGenerator : ILevelGenerator
    {
        private readonly ILevelEnemiesHolder _enemies;
        private readonly IBlockGenerator _blockGenerator;
        private readonly IRunRoadGenerator _roadGenerator;
        private readonly IEntityGenerator _entityGenerator;
        private readonly IFloorGenerator _floorGenerator;
        private readonly IStairwayGenerator _stairwayGenerator;
        private LevelFloor _enemyFloor;
        private LevelFloor _playerFloor;
        private bool _isAfterRunning;
        private Vector3 _runningEndPos;



        public LevelGenerator(IFloorGenerator floorGenerator, IEntityGenerator entityGenerator,
            IStairwayGenerator stairwayGenerator, ILevelEnemiesHolder enemies,IBlockGenerator blockGenerator,
            IRunRoadGenerator roadGenerator)
        {
            _floorGenerator = floorGenerator;
            _entityGenerator = entityGenerator;
            _stairwayGenerator = stairwayGenerator;
            _enemies = enemies;
            _blockGenerator = blockGenerator;
            _roadGenerator = roadGenerator;
            _isAfterRunning = false;
        }

        public void CreateLevelStart()
        {
            _floorGenerator.ChooseLevelStyle();
            _playerFloor = _floorGenerator.GenerateNextFloor();
            _enemyFloor = _floorGenerator.GenerateNextFloor();
            GenerateStairway();
            _enemies.Player = _entityGenerator.GeneratePlayer(_playerFloor);
            UpdateEnemy();
            _blockGenerator.UpdateBlockView();
        }
        public void UpdateLevel(Action callback)
        {
            if (_isAfterRunning)
            {
                UpdateLevelAfterRun();
                _isAfterRunning = false;
            }
            else
            {
                StandardLevelUpdate();
            }
            callback.Invoke();
        }

        private void StandardLevelUpdate()
        {
            _playerFloor = _enemyFloor;
            _enemies.Player.Mover.SetNextFloor(_playerFloor);
            _enemyFloor = _floorGenerator.GenerateNextFloor();
            UpdateEnemy();
            GenerateStairway();
        }

        private void UpdateLevelAfterRun()
        {
            _blockGenerator.DeactivateAutoBlockUpdate();
            _enemies.Player.Mover.SetDefault();
            _enemies.ActiveEnemy?.Mover.SetDefault();
            _floorGenerator.SetGenerationStartPosition(_runningEndPos + Vector3.up*100f);
            _playerFloor = _floorGenerator.GenerateNextFloor();
            _enemyFloor = _floorGenerator.GenerateNextFloor();
            _enemies.Player.Mover.SetNextFloor(_playerFloor);
            GenerateStairway();
            UpdateEnemy();
            _enemies.Player.Mover.TeleportToActiveLevel();
            _enemies.ActiveEnemy.Mover.TeleportToActiveLevel();
            _blockGenerator.UpdateBlockView();
            
        }
        private void GenerateStairway()
        {
            _stairwayGenerator.GenerateStairway(_floorGenerator.LevelStyleIndex, _playerFloor.GetStairConnectPosition(_enemyFloor.GetSpawnPosition()).Position,
                _enemyFloor.GetStairConnectPosition(_playerFloor.GetSpawnPosition()).Position);
        }

        public void ClearLevel()
        {
            _enemies.ClearLevel();
            _blockGenerator.ClearLevel();
        }

        public int GenerateRunningWay()
        {
            _blockGenerator.ActivateAutoBlockUpdate();
            _isAfterRunning = true;
            Vector3 start = _enemyFloor.GetSpawnPosition();
            if (_enemyFloor.GetSpawnPosition().x > _playerFloor.GetSpawnPosition().x)
            {
                start = _enemyFloor.GetSpawnPosition();
                _runningEndPos = _roadGenerator.GenerateStandardRoad(start, 2, _floorGenerator.LevelStyleIndex);
                return -1;
            }
            _runningEndPos = _roadGenerator.GenerateStandardRoad(start, 3, _floorGenerator.LevelStyleIndex);
            return 1;
        }
        private void UpdateEnemy()
        {
            if (_enemies.ActiveEnemy == null)
            {
                _enemies.ActiveEnemy = _entityGenerator.GenerateEnemy(_enemyFloor);
                _enemies.ActiveEnemy.EnemyHealth.OnDead += EnemyDead;
            }
            else
            {
                _enemies.ActiveEnemy.Mover.SetNextFloor(_enemyFloor);
            }
        }

        private void EnemyDead()
        {
            _enemies.ActiveEnemy.EnemyHealth.OnDead -= EnemyDead;
            _enemies.ActiveEnemy = null;
        }
    }
}