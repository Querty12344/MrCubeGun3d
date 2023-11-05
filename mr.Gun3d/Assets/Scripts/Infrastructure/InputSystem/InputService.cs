using System;
using UnityEngine;

namespace Infrastructure.InputSystem
{
    public class InputService : MonoBehaviour, IInputService
    {
        private bool _shootingIsActive;
        private event Action StartShooting;
        private event Action Jump;

        public void Init(Action jump)
        {
            Jump += jump;
        }

        public void DeactivateShooting()
        {
            StartShooting = null;
            _shootingIsActive = false;
        }

        public void ActivateShooting(Action shoot)
        {
            StartShooting += shoot;

            _shootingIsActive = true;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && _shootingIsActive) StartShooting?.Invoke();
            if (Input.GetKeyDown(KeyCode.Space) && _shootingIsActive) Jump?.Invoke();
        }
    }
}