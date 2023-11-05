using UnityEngine;

namespace Infrastructure.EffectsManagement
{
    public interface IGameEffects
    {
        void PlayCriticalDamageEffect();
        void PlayDamageEffect();
        void PlayCoinEffect(Vector3 coinSpawnPosition, int moneyCount);
    }
}