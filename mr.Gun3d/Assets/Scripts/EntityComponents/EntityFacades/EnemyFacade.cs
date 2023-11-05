using System;
using EntityComponents.EnemyAwardSystem;
using EntityComponents.Health;
using EntityComponents.Movement;
using EntityComponents.ShootingSystem;
using UnityEngine;

namespace EntityComponents.EntityFacades
{
    public class EnemyFacade : MonoBehaviour
    {
        public IEntityAnimator Animator;
        public IEnemyAward Award;
        public IShooter EnemyShooter;
        public IGunHolder GunHolder;
        [NonSerialized] public Health.EnemyHealth EnemyHealth;
        public IEntityMover Mover;
        public GunTypeId GunType;

        public void Construct(GunTypeId gunTypeId)
        {
            GunType = gunTypeId;
            EnemyHealth = GetComponent<Health.EnemyHealth>();
            Mover = GetComponent<IEntityMover>();
            GunHolder = GetComponent<IGunHolder>();
            Animator = GetComponent<IEntityAnimator>();
            Award = GetComponent<IEnemyAward>();
        }

        public void Remove()

        {
            Destroy(gameObject);
        }

        public void RunAwayFromLevel()
        {
            EnemyHealth.ApplyDamage(10000f);
        }
    }
}