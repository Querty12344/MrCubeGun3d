using UnityEngine;

namespace EnvironmentComponents
{
    public class Coin : MonoBehaviour
    {
        public void Construct(float lifeTime)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}