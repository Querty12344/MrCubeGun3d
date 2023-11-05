using System;
using EntityComponents.Movement;
using UnityEngine;

namespace EnvironmentComponents
{
    public class JumpTrigger:MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IEntityMover>(out var mover))
            {
                mover.JumpFromTrigger(transform.position);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IEntityMover>(out var mover))
            {
                mover.JumpFromTrigger(transform.position);
            }
        }
    }
}