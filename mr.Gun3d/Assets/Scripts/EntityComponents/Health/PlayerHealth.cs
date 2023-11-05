using System;
using Infrastructure.EffectsManagement;
using UnityEngine;

namespace EntityComponents.Health
{
    public class PlayerHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private DamageTrigger _bodyDamageTrigger;
        private IGameEffects _gameEffects;
        private float _health;
        private float _maxHealth;

        public bool IsAlive => _health >= 0;


        public void Construct(float health, IGameEffects gameEffects)
        {
            _maxHealth = health;
            _health = health;
            _gameEffects = gameEffects;
            _bodyDamageTrigger.Construct(ApplyDamage);
        }


        public event Action OnDead;
        public event Action OnCriticalDamage;
        public event Action<float> OnChanged;

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            OnChanged?.Invoke(_health/_maxHealth);
            _gameEffects.PlayDamageEffect();
            if (_health <= 0f)
            {
                OnDead?.Invoke();
            }
        }
    }
}