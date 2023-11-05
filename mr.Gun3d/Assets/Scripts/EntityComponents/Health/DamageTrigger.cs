using System;
using UnityEngine;

namespace EntityComponents.Health
{
    public class DamageTrigger : MonoBehaviour, IDamageTrigger
    {
        private Action<float> _takeDamage;

        public void TakeDamage(float damage)
        {
            _takeDamage.Invoke(damage);
        }

        public void Construct(Action<float> takeDamage)
        {
            _takeDamage = takeDamage;
        }
    }
}