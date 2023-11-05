using EntityComponents.EntityFacades;
using EnvironmentComponents;

namespace Infrastructure.Factories
{
    public interface IEntityFactory
    {
        EnemyFacade CreateRandomEnemy(LevelFloor floor);
        PlayerFacade InstantiatePlayer(LevelFloor floor);
    }
}