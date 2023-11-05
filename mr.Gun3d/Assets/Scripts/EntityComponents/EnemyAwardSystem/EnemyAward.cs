using EntityComponents.Health;
using Infrastructure.EffectsManagement;
using Infrastructure.MoneyManagement;
using Infrastructure.ScoreSystem;
using UnityEngine;

namespace EntityComponents.EnemyAwardSystem
{
    public class EnemyAward : MonoBehaviour, IEnemyAward
    {
        private IGameEffects _gameEffects;
        private IMoney _money;
        private int _moneyAward;
        private int _smallMoneyAward;
        private int _scoreAward;
        private IScore _score;

        public void Construct(IHealth health,
            IGameEffects gameEffects,
            IMoney money,
            IScore score,
            int moneyAward,
            int smallMoneyAward,
            int scoreAward)
        {
            _score = score;
            _scoreAward = scoreAward;
            _smallMoneyAward = smallMoneyAward;
            _gameEffects = gameEffects;
            _money = money;
            _moneyAward = moneyAward;
            health.OnDead += GiveAward;
            health.OnCriticalDamage += GiveSmallAward;
        }

        private void GiveAward()
        {
            _score.AddScore(_scoreAward);
            _gameEffects.PlayCoinEffect(transform.position, _moneyAward);
            _money.AddMoney(_moneyAward);
        }
        private void GiveSmallAward()
        {
            _gameEffects.PlayCoinEffect(transform.position, _smallMoneyAward);
            _money.AddMoney(_smallMoneyAward);
        }
    }
}