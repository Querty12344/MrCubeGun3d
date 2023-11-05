using UnityEngine;

namespace EntityComponents.Movement
{
    public class EntityAnimator : MonoBehaviour, IEntityAnimator
    {
        [SerializeField] private Animator _animator;

        public void OnMovement()
        {
            _animator.SetBool("IsMoving", true);
            _animator.SetBool("IsWaiting", false);
        }
        

        public void OnWaiting()
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsWaiting", true);
        }

        public void Death()
        {
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsWaiting", false);
            _animator.SetBool("IsDeath", true);
        }
    }
}