using System.Collections.Generic;
using System.Linq;
using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.LevelEnvironmentSystem.LevelGeneration;
using Infrastructure.Random;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    public class StaticData
    {
        private GameDifficultyStaticData _difficulty;
        private EnemyStaticData[] _enemyStaticData;
        private GunStaticData[] _guns;
        private PlayerStaticData[] _playerStaticData;
        private RunningEndTrigger _runningEndTrigger;
        private List<Block> _blocks;
        private List<Block> _environmentBlocks;
        private List<Block> _lightEnvironmentBlocks;
        private Transform _empty;
        private GenerationStaticData _settings;
        private Block _trap;
        private GameObject _guide;

        public StaticData(GameSettingsProvider settingsProvider)
        {
            _settings = settingsProvider.GenerationSettings;
        }

        public void LoadStaticData()
        {
            _guide = Resources.Load<GameObject>(ResourcesPath.Guide);
            _trap = Resources.Load<Block>(ResourcesPath.Trap);
            _blocks = Resources.LoadAll<Block>(ResourcesPath.Blocks).ToList();
            _environmentBlocks = Resources.LoadAll<Block>(ResourcesPath.Environment).ToList();
            _lightEnvironmentBlocks = Resources.LoadAll<Block>(ResourcesPath.LiteEnvironment).ToList();
            _enemyStaticData = Resources.LoadAll<EnemyStaticData>(ResourcesPath.Enemies);
            _playerStaticData = Resources.LoadAll<PlayerStaticData>(ResourcesPath.Players);
            _guns = Resources.LoadAll<GunStaticData>(ResourcesPath.Guns);
            _difficulty = Resources.Load<GameDifficultyStaticData>(ResourcesPath.Difficulty);
            _runningEndTrigger = Resources.Load<RunningEndTrigger>(ResourcesPath.RunningEndTrigger);
            _empty = Resources.Load<Transform>(ResourcesPath.Empty);
        }
        

        public EnemyStaticData GetRandomEnemy()
        {
            return _enemyStaticData[Randomizer.Range(0, _enemyStaticData.Length)];
        }

        public PlayerStaticData GetPlayer()
        {
            return _playerStaticData[0];
        }
        

        public GunStaticData[] GetAllGuns()
        {
            return _guns;
        }

        public GameDifficultyStaticData GetGameDifficulty()
        {
            return _difficulty;
        }
        
        public Block GetBlock(int style)
        {
            return _blocks[style];
        }

        public RunningEndTrigger GetRunningEndTrigger()
        {
            return _runningEndTrigger;
        }

        public Transform GetEmpty()
        {
            return _empty;
        }

        public Block GetRandomEnvironment(int style)
        {
            int r = 0;
            while (true)
            {
                if (_settings.GreenStyles.Contains(style))
                {
                    r = Randomizer.Range(0,_environmentBlocks.Count);
                    break;  
                }
                r = Randomizer.Range(0,_environmentBlocks.Count);
                if (!_settings.GreenEnvStyles.Contains(r))
                {
                    break;
                }
            }
            return _environmentBlocks[r];
        }

        public Block GetRandomLightEnvironment(int style)
        {
            int r = 0;
            while (true)
            {
                if (_settings.GreenStyles.Contains(style))
                {
                    r = Randomizer.Range(0,_lightEnvironmentBlocks.Count);
                    break;  
                }
                r = Randomizer.Range(0,_lightEnvironmentBlocks.Count);
                if (!_settings.GreenLEnvStyles.Contains(r))
                {
                    break;
                }
            }
            return _lightEnvironmentBlocks[r];
        }

        public Block GetTrap()
        {
            return _trap;
        }

        public GameObject GetGuide()
        {
            return _guide;
        }
    }
}