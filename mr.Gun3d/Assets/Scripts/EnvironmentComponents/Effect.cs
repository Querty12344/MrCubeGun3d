using UnityEngine;

namespace EnvironmentComponents
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private float _startTime;

        private void Awake()
        {
            _startTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _startTime > _lifeTime) Destroy(gameObject);
        }
    }
}