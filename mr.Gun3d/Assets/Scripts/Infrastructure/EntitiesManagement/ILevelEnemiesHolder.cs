using EntityComponents.EntityFacades;

namespace Infrastructure.EntitiesManagement
{
    public interface ILevelEnemiesHolder
    {
        EnemyFacade ActiveEnemy { get; set; }
        PlayerFacade Player { get; set; }
        void ClearLevel();
    }
}