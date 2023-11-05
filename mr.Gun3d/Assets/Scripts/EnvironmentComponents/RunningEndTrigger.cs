using System;
using EntityComponents.Movement;
using UnityEngine;

namespace EnvironmentComponents
{
    public class RunningEndTrigger:MonoBehaviour
    {
        public void Construct()
        {
            
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IEntityMover>(out var entityMover))
            {
                entityMover.StopRunning();
            }
        }
    }
}