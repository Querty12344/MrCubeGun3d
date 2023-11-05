using UnityEngine;

namespace EnvironmentComponents
{
    public class LevelStartPosition : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Vector3 GetPosition()
        {
            return FindObjectOfType<LevelStartPositionMarker>().transform.position;
        }
    }
}