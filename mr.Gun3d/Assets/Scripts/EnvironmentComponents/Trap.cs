using System;
using EntityComponents.Health;
using UnityEngine;

namespace EnvironmentComponents
{
    public class Trap:MonoBehaviour
    {
        [SerializeField] private float _damage;
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.ApplyDamage(_damage);
            }
        }
    }
}