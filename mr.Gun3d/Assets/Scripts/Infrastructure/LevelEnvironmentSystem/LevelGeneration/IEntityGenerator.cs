using EntityComponents.EntityFacades;
using EnvironmentComponents;

namespace Infrastructure.LevelEnvironmentSystem.LevelGeneration
{
    public interface IEntityGenerator
    {
        EnemyFacade GenerateEnemy(LevelFloor floor);
        PlayerFacade GeneratePlayer(LevelFloor floor);
    }
}