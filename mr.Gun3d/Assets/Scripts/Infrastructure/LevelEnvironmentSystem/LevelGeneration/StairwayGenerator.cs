using System;
using System.Collections;
using System.Collections.Generic;
using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class StairwayGenerator : IStairwayGenerator
    {
        private readonly IBlockGenerator _blockGenerator;
        private readonly GenerationStaticData _settings;

        public StairwayGenerator(GameSettingsProvider settingsProvider ,IBlockGenerator blockGenerator)
        {
            _settings = settingsProvider.GenerationSettings;
            _blockGenerator = blockGenerator;
        }

        public void GenerateStairway(int styleIndex,Vector3 start, Vector3 end)
        {
            var count = CalculateCount(start, end, _settings.BlockOffset);
            var xOffset = end.x - start.x > 0 ? _settings.BlockOffset : -_settings.BlockOffset;
            var offset = new Vector3(xOffset, CalculateYOffset(start, end, count), 0);
            for (var i = 0; i < count; i++)
            {
                _blockGenerator.GenerateBlock(start + offset * (i + 1),styleIndex);
            }
        }
        
        private int CalculateCount(Vector3 start, Vector3 end, float stepXOffset)
        {
            var xLength = Mathf.Abs(end.x - start.x);
            return (int)(xLength / stepXOffset);
        }

        private float CalculateYOffset(Vector3 start, Vector3 end, int count)
        {
            return (end.y - start.y) / count;
        }
    }
}