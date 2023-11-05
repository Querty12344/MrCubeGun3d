using System;
using EntityComponents.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI.UIElements
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _indicator;
        private IHealth _health;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Activate(IHealth health)
        {
            _health = health;
            gameObject.SetActive(true);
            health.OnChanged += UpdateView;
            health.OnDead += Deactivate;
            UpdateView(1);
        }

        private void Deactivate()
        {
            _health.OnChanged += UpdateView;
            _health.OnDead += Deactivate;
            gameObject.SetActive(false);
        }
        private void UpdateView(float healthPercent)
        {
            _indicator.fillAmount = healthPercent;
        }
    }
}