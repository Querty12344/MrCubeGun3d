using EntityComponents.Health;
using EntityComponents.Movement;
using EntityComponents.ShootingSystem;
using UnityEngine;

namespace EntityComponents.EntityFacades
{
    public class PlayerFacade : MonoBehaviour
    {
        public PlayerHealth Health;
        public IEntityAnimator Animator;
        public IGunHolder GunHolder;
        public IEntityMover Mover;
        public IShooter InRunShooter;
        public IShooter OnStairsShooter;

        public void Construct()
        {
            Health = GetComponent<PlayerHealth>();
            Mover = GetComponent<IEntityMover>();
            GunHolder = GetComponent<IGunHolder>();
            Animator = GetComponent<EntityAnimator>();
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}