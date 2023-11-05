using UnityEngine;

namespace EnvironmentComponents
{
    public class WayPoint
    {
        public Vector3 Position;
        public Vector3 ForEnemyOffset;

        public WayPoint(Vector3 position, Vector3 forEnemyOffset)
        {
            Position = position;
            ForEnemyOffset = forEnemyOffset;
        }
    }
}