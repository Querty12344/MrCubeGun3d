using UnityEngine;

namespace EntityComponents.Movement
{
    public class PhysicsActivator
    {
        private Rigidbody _rigidbody;
        private Collider _boxCollider;

        public PhysicsActivator(Rigidbody rigidbody, Collider boxCollider)
        {
            _rigidbody = rigidbody;
            _boxCollider = boxCollider;
        }

        public void SetStartSettings()
        {
            _boxCollider.isTrigger = true;
            _rigidbody.useGravity = false;
        }
        public void ActivatePhysics()
        {
            _boxCollider.isTrigger = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void DeactivatePhysics()
        {
            _boxCollider.isTrigger = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}