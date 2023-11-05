using System;

namespace EntityComponents.Health
{
    public interface IHealth
    {
        public event Action OnDead;
        public event Action OnCriticalDamage;
        public event Action<float> OnChanged;
        public void ApplyDamage(float damage);
    }
}