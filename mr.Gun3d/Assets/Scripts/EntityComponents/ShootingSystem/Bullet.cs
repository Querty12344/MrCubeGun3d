using EntityComponents.Health;
using Infrastructure.ResourceManagement.StaticData;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        private Vector3 _direction;
        private BulletStaticData _settings;
        private float _startTime;
        private float _damage;

        private void Start()
        {
            transform.SetParent(null);
            if (_settings.StartEffect != null)
                Instantiate(_settings.StartEffect, transform);

            if (_settings.ConstantEffect != null)
                Instantiate(_settings.ConstantEffect, transform);
            _startTime = Time.time;
        }

        private void FixedUpdate()
        {
            Move();
            if (Time.time - _startTime > _settings.BulletLifeTime) Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_settings.CollisionEffect != null)
                Instantiate(_settings.CollisionEffect, transform.position, Quaternion.identity);
            if (other.TryGetComponent<IDamageTrigger>(out var damageTrigger))
            {
                HitTarget(damageTrigger);
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
        }


        public void Construct(Vector3 direction, float damage,BulletStaticData settings)
        {
            _damage = damage;
            _settings = settings;
            _direction = direction;
        }

        protected virtual void Move()
        {
            var offset = _direction.normalized *_settings.Speed;
            _rigidBody.MovePosition(transform.position + offset);
        }

        protected virtual void HitTarget(IDamageTrigger damageTrigger)
        {
            damageTrigger.TakeDamage(_damage);
        }
    }
}