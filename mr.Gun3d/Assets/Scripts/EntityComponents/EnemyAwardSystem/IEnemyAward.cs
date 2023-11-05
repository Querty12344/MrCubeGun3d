using EntityComponents.Health;
using Infrastructure.EffectsManagement;
using Infrastructure.MoneyManagement;
using Infrastructure.ScoreSystem;

namespace EntityComponents.EnemyAwardSystem
{
    public interface IEnemyAward
    {
        public void Construct(IHealth health,
            IGameEffects gameEffects,
            IMoney money,
            IScore score,
            int moneyAward,
            int smallMoneyAward,
            int scoreAward);
    }
}