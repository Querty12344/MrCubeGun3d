using System.Collections.Generic;
using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.Random;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class FloorGenerator : IFloorGenerator
    {
        private readonly IBlockGenerator _blockGenerator;
        private readonly Vector3 _levelStartPosition;
        private readonly GenerationStaticData _parameters;
        private float _lastFloorRotation = 180f;
        private Vector3 _nextFloorPosition;
        private bool _rotationCof;
        public int LevelStyleIndex { get; set; }

        public FloorGenerator(IBlockGenerator blockGenerator,
            LevelStartPosition startPosition,
            GameSettingsProvider settingsProvider
        )
        {
            _blockGenerator = blockGenerator;
            _parameters = settingsProvider.GenerationSettings;
            _nextFloorPosition = startPosition.GetPosition();
            _levelStartPosition = startPosition.GetPosition();
        }

        public void ChooseLevelStyle()
        {
            LevelStyleIndex = Randomizer.Range(1,_parameters.AvailableBlockCount) ;
        }

        public void SetGenerationStartPosition(Vector3 at)
        {
            _nextFloorPosition = at;
            _lastFloorRotation = 0f;
        }

        public LevelFloor GenerateNextFloor()
        {
            int floorRotCof = 0;
            if (_lastFloorRotation / 180f % 2 == 0)
            {
                floorRotCof = 1;
            }
            
            var floor = _blockGenerator.GenerateBlockPlatform(_nextFloorPosition,LevelStyleIndex,floorRotCof);
            
            
            var XOffset = Randomizer.Range(_parameters.MinXOffset, _parameters.MaxXOffset);
            var YOffset = Randomizer.Range(_parameters.MinYOffset, _parameters.MaxYOffset);
            var finalOffset = new Vector3(XOffset, YOffset, 0);
            _nextFloorPosition += Quaternion.AngleAxis(_lastFloorRotation, Vector3.up) * finalOffset;
            _lastFloorRotation += 180f;
            return floor;
        }
        

        public void ClearLevel()
        {
            _nextFloorPosition = _levelStartPosition;
            _lastFloorRotation = 0f;
        }
    }
}