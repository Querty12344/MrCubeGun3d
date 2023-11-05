using Infrastructure.Constants;
using UnityEngine;

namespace EntityComponents.Movement
{
    public class Jumper:MonoBehaviour
    {
        private float _jumpForce;
        private int _grounded;
        private EntitySettings _settings;
        private bool _isActive;

        public void Activate()
        {
            _isActive = true;
            _grounded = 0;
            _jumpForce = _settings.JumpForce ;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
        public void Construct(EntitySettings settings)
        {
            _settings = settings;
        }

        public void Jump()
        {
            if (_isActive && _grounded > 0)
            {
                _grounded = 0;
                _jumpForce = _settings.JumpForce ;
            }
        }
        public Vector3 GetJumpOffset()
        {
            Vector3 offset = Vector3.up * _settings.JumpOffset * _jumpForce;
            if (_jumpForce == _settings.JumpForce)
            {
                offset = Vector3.up * _settings.StartJumpOffset;
            }
            _jumpForce -= _settings.JumpOffset;
            if (_grounded > 0)
            {
                _jumpForce = 0f;
            }

            return offset;
        }
        public void JumpFromTrigger()
        {
            if (_isActive)
            {
                _jumpForce = _settings.JumpForce;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground") && _jumpForce <= 0f )
            {
                _grounded++;
                _jumpForce = 0f;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (_grounded > 0)
                {
                    _grounded--;
                }
            }
        }
    }
}