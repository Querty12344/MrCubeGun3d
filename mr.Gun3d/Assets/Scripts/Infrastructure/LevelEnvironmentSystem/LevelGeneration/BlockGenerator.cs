using System;
using System.Collections;
using System.Collections.Generic;
using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.EntitiesManagement;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.Random;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class BlockGenerator:IBlockGenerator
    {
        private readonly ILevelEnvironmentFactory _levelFactory;
        private readonly ILevelEnemiesHolder _enemies;
        private readonly ICoroutineRunner _coroutineRunner;
        private List<Block> _blocks;
        private List<Transform> _blocksToInstantiate;
        private List<Transform> _environmentToInstantiate;
        private List<Transform> _liteEnvironmentToInstantiate;
        private List<Transform> _trapsEnvironmentToInstantiate;
        private GenerationStaticData _settings;
        private OptimizationSettings _optimizationSettings;
        private Vector3 _endPosition;
        private bool _autoUpdate;
        private int _style;

        public BlockGenerator(GameSettingsProvider settingsProvider,ILevelEnvironmentFactory levelFactory
            ,ILevelEnemiesHolder enemies,ICoroutineRunner coroutineRunner)
        {
            _settings = settingsProvider.GenerationSettings;
            _optimizationSettings = settingsProvider.OptimizationSettings;
            _levelFactory = levelFactory;
            _enemies = enemies;
            _coroutineRunner = coroutineRunner;
            _blocks = new List<Block>();
            _blocksToInstantiate = new List<Transform>();
            _environmentToInstantiate = new List<Transform>();
            _liteEnvironmentToInstantiate = new List<Transform>();
            _trapsEnvironmentToInstantiate = new List<Transform>();
        }
        public void UpdateBlockView()
        {
            if (_enemies.Player == null) return;
            List<Block> removing = new List<Block>();
            foreach (Block b in _blocks)
            {
                if (IsNear(b.transform.position))
                {
                    b.Activate();
                }
                else
                {
                    removing.Add(b);
                    b.Deactivate();
                }
            }

            foreach (Block b in removing)
            {
                _blocks.Remove(b);
            }
            removing.Clear();
            InstantiateNextBlocks(_blocksToInstantiate,createByStyle:_levelFactory.CreateBlock);
            InstantiateNextBlocks(_environmentToInstantiate,_levelFactory.CreateEnvironment);
            InstantiateNextBlocks(_liteEnvironmentToInstantiate,_levelFactory.CreateLightEnvironment);
            InstantiateNextBlocks(_trapsEnvironmentToInstantiate,_levelFactory.CreateTrap);
        }

        private void InstantiateNextBlocks(List<Transform> blocks,Func<Vector3,int,Block> createByStyle = null)
        {
            List<Transform> instantiated = new List<Transform>();
            foreach (Transform b in blocks)
            {
                if (b != null)
                {
                    if (IsNear(b.position))
                    {
                        instantiated.Add(b);
                        Block next =createByStyle.Invoke(b.position, _style);
                        _blocks.Add(next);
                    }
                }
            }
            
            foreach (var b in instantiated)
            {
                blocks.Remove(b);
                GameObject.Destroy(b.gameObject);
            }
            
            instantiated.Clear();
        }

        private bool IsNear(Vector3 pos)
        {
            Vector3 entityPos;
            if (_enemies.ActiveEnemy == null)
            {
                entityPos = _enemies.Player.transform.position;
            }
            else
            {
                entityPos = _enemies.ActiveEnemy.transform.position;
            }
            float yDistance = entityPos.y - pos.y;
            float toEnemyDist = Mathf.Abs(pos.x - entityPos.x);
            return toEnemyDist < _optimizationSettings.BlockDisableDistance &&
                   yDistance < _optimizationSettings.MaxYOffset;
        }

        public LevelFloor GenerateBlockPlatform(Vector3 at, int style, int rotationCof, int length = 0,
            int width = 0, int height = 0)
        {
            _style = style;
            if (length == 0)
            {
                length = _settings.DefaultFloorLength;
            }

            if (width == 0) 
            {
                width = _settings.DefaultFloorWidth;
            }
            if (height == 0)
            {
                height= _settings.DefaultFloorHeight;
            }

            Transform root = _levelFactory.CreateEmpty(at);
            if (length > 10)
            {
                _levelFactory.CreateGuide(root);
            }
            Block outBlock = null;
            Block shootingPosBlock = null;
            Block lastBlock = null;
            int noiseOffset = 0;
            for (int l = 0; l < length; l++)
            {
                if (l % _settings.RoadСurvature == 0 && length > 10 && l>2)
                {
                    noiseOffset += Randomizer.Range(-1,2);   
                }
                if (noiseOffset > 1)
                {
                    noiseOffset = 1;
                }
                for (int w = 0;w  < width;w ++)
                {
                    for (int h = noiseOffset; h < height; h++)
                    {
                        bool isImportant = false;
                        Vector3 offset = new Vector3(l,-h,w)*_settings.BlockOffset;
                        if (w == Mathf.RoundToInt(width/2) && l == 0 && h == noiseOffset)
                        {
                            isImportant = true;
                            Block next = CreateBlock(at + offset,root);
                            outBlock = next;
                        }
                        if(l ==Mathf.RoundToInt(length/2) && w == Mathf.RoundToInt(width/2) && h == noiseOffset)
                        {
                            isImportant = true;
                            Block next = CreateBlock(at + offset,root);
                            shootingPosBlock = next;
                        }

                        if (h == noiseOffset && l == length - 1 && w == 0)
                        {
                            isImportant = true;
                            Block next = CreateBlock(at + offset,root);
                            lastBlock = next;
                        }

                        if (!isImportant)
                        {
                            CreateBlockTransform(at + offset,root);
                        }

                        if (!isImportant && Randomizer.Range(1, 100) < _settings.TrapChance && length > 10 && w < 1 &&
                            rotationCof == 2)
                        {
                            Vector3 environmentOffset = Vector3.up * _settings.BlockOffset;
                            CreateEnvironment(at + offset + environmentOffset,3,root);
                        }
                        else if(!isImportant && Randomizer.Range(1, 100) < _settings.TrapChance && length > 10 && w > 1 &&
                                rotationCof == 3)
                        {
                            Vector3 environmentOffset = Vector3.up * _settings.BlockOffset;
                            CreateEnvironment(at + offset + environmentOffset,3,root);
                        }
                        else if (Randomizer.Range(1,100) < _settings.EnvChance && length > 10 && w > 1 && rotationCof == 2)
                        {
                            Vector3 environmentOffset = Vector3.up * _settings.BlockOffset;
                            CreateEnvironment(at + offset + environmentOffset,2,root);
                        }
                        else if (Randomizer.Range(1, 100) < _settings.EnvChance && length > 10 && w < 1 && rotationCof == 3)
                        {
                            Vector3 environmentOffset = Vector3.up * _settings.BlockOffset;
                            CreateEnvironment(at + offset + environmentOffset,2,root);
                        }
                        else if(Randomizer.Range(1,100) < _settings.LightEnvChance) 
                        {
                            CreateEnvironment(at + offset,1,root);
                        }

                        if (h == noiseOffset )
                        {
                            break;
                        }
                    }
                }
            }
            if (rotationCof == 1)
            {
                Vector3 floorOffset = new Vector3(-length +1,0,0) * _settings.BlockOffset;
                root.position = at + floorOffset;
                root.transform.rotation = Quaternion.Euler(0,-90f,0);
            }
            if (rotationCof == 2)
            {
                Vector3 floorOffset = new Vector3(2,0,0) * _settings.BlockOffset - _settings.EnemyOnOffset;
                root.transform.position = at + floorOffset;
                root.transform.rotation = Quaternion.Euler(0,0f,0);
            }
            if (rotationCof == 3)
            {
                Vector3 floorOffset = new Vector3(-4,0,2) * _settings.BlockOffset - _settings.EnemyOnOffset;
                root.transform.position = at + floorOffset;
                root.transform.rotation = Quaternion.Euler(0,180f,0);
            }
            WayPoint inPoint = new WayPoint(root.transform.position,_settings.EnemyOnOffset);
            WayPoint outPoint = new WayPoint(outBlock.transform.position ,_settings.EnemyOnOffset);
            WayPoint shootingPos = new WayPoint(shootingPosBlock.transform.position, _settings.EnemyOnOffset);
            LevelFloor levelFloor = new LevelFloor(inPoint,outPoint,shootingPos);
            _endPosition = lastBlock.transform.position;
            UpdateBlockView();
            return levelFloor;
        }

        private Block CreateBlock(Vector3 at,Transform root)
        {
            Block next = _levelFactory.CreateBlock(at,_style);
            _blocks.Add(next);
            next.transform.SetParent(root);
            return next;
        }

        private void CreateBlockTransform(Vector3 at,Transform root)
        {
            Transform next = _levelFactory.CreateEmpty(at);
            _blocksToInstantiate.Add(next);
            next.transform.SetParent(root);
        }

        private void CreateEnvironment(Vector3 at,int type,Transform root)
        {
            Transform next = _levelFactory.CreateEmpty(at);
            switch (type)
            {
                case 1:
                    _liteEnvironmentToInstantiate.Add(next);
                    break;
                case 2:
                    _environmentToInstantiate.Add(next);
                    break;
                case 3:
                    _trapsEnvironmentToInstantiate.Add(next);
                    break;
            }
            next.transform.SetParent(root);
        }
        public void ActivateAutoBlockUpdate()
        {
            _autoUpdate = true;
            _coroutineRunner.StartCoroutine(BlockAutoUpdate());

        }

        public void DeactivateAutoBlockUpdate() => _autoUpdate = false;
        public void GenerateBlock(Vector3 at, int style)
        {
            _blocks.Add(_levelFactory.CreateBlock(at,style));
        }
        public void ClearLevel()
        {
            foreach (var block in _blocks)
            {
                block.Remove();
            }
            _blocks.Clear();
        }

        public Vector3 GetLastRoadEndPos() => _endPosition;

        private IEnumerator BlockAutoUpdate()
        {
            while (_autoUpdate)
            {
                UpdateBlockView();
                yield return new WaitForSeconds(_optimizationSettings.BlockUpdateTiming);
            } 
        }
    }
}





