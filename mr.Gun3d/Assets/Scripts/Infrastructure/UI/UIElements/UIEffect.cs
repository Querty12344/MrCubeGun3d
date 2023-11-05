using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class UIEffect : MonoBehaviour
    {
        private float _lifeTime;
        private float _startTime;

        private void Update()
        {
            if (Time.time - _startTime > _lifeTime) Destroy(gameObject);
        }

        public void Construct(float lifeTime)
        {
            _startTime = Time.time;
            _lifeTime = lifeTime;
        }
    }
}