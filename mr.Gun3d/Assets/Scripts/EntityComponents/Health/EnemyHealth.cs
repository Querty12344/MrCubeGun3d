using System;
using DG.Tweening;
using EntityComponents.Movement;
using Infrastructure.EffectsManagement;
using Infrastructure.Random;
using UnityEngine;
using Random = System.Random;

namespace EntityComponents.Health
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private DamageTrigger _bodyDamageTrigger;
        [SerializeField] private float _criticalDamageModifier;
        [SerializeField] private DamageTrigger _headDamageTrigger;
        private IGameEffects _gameEffects;
        private float _health;
        private float _maxHealth;
        private IEntityMover _mover;
        private float _runningHealthModifer;
        private int _critAwardChance;
        public event Action OnDead;
        public event Action OnCriticalDamage;
        public event Action<float> OnChanged;


        public void Construct(IEntityMover mover,float health, float runningHealthModifer,float criticalDamageModifier,int critAwardChance
            , IGameEffects gameEffects)
        {
            _critAwardChance = critAwardChance;
            _runningHealthModifer = runningHealthModifer;
            _mover = mover;
            _gameEffects = gameEffects;
            _criticalDamageModifier = criticalDamageModifier;
            _health = health;
            _maxHealth = health;
            _headDamageTrigger.Construct(ApplyCriticalDamage);
            _bodyDamageTrigger.Construct(ApplyDamage);
        }

        public void ApplyDamage(float damage)
        {
            if (_mover is { IsRunning: true })
            {
                damage *= _runningHealthModifer;
            }
            _health -= damage;
            if (_health <= 0f) Die();
            _gameEffects.PlayDamageEffect();
            OnChanged?.Invoke(_health/_maxHealth);
        }

        public void ApplyCriticalDamage(float damage)
        {
            float startDamage = damage;
            if (_mover is { IsRunning: true })
            {
                damage *= _runningHealthModifer;
            }
            _health -= damage * _criticalDamageModifier;
            if (_health <= 0f) Die();
            _gameEffects.PlayCriticalDamageEffect();
            if (Randomizer.Range(0, 100) < startDamage * _critAwardChance)
            {
                OnCriticalDamage?.Invoke();
            }
            OnChanged?.Invoke(_health/_maxHealth);
        }

        private void Die()
        {
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
}