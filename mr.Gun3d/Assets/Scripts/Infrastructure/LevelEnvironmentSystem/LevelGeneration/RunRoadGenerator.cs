
using Infrastructure.Factories;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.SettingsManagement;
using UnityEngine;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public class RunRoadGenerator : IRunRoadGenerator
    {
        private readonly IBlockGenerator _blockGenerator;
        private readonly ILevelEnvironmentFactory _levelEnvironmentFactory;
        private readonly GenerationStaticData _settings;

        public RunRoadGenerator(IBlockGenerator blockGenerator, GameSettingsProvider settingsProvider,
            ILevelEnvironmentFactory levelEnvironmentFactory)
        {
            _blockGenerator = blockGenerator;
            _levelEnvironmentFactory = levelEnvironmentFactory;
            _settings = settingsProvider.GenerationSettings;
        }

        public Vector3 GenerateStandardRoad(Vector3 start, int rotationCof, int style)
        {
            _blockGenerator.GenerateBlockPlatform(start, style, rotationCof, _settings.roadLength, _settings.roadWidth,
                _settings.roadHeight);
            Vector3 end =  _blockGenerator.GetLastRoadEndPos();
            _levelEnvironmentFactory.CreateRunningEndTrigger(end);
            return end;
        }

        public void GenerateRoadFinish(Vector3 end)
        {
            _levelEnvironmentFactory.CreateRunningEndTrigger(end);
        }
    }
}