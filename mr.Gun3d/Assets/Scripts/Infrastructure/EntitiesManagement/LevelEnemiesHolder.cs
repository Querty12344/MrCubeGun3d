using EntityComponents.EntityFacades;

namespace Infrastructure.EntitiesManagement
{
    public class LevelEnemiesHolder : ILevelEnemiesHolder
    {
        public EnemyFacade ActiveEnemy { get; set; }
        public PlayerFacade Player { get; set; }

        public void ClearLevel()
        {
            ActiveEnemy?.Remove();
            Player?.Remove();
            ActiveEnemy = null;
            Player = null;
        }
    }
}