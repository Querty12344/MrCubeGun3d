using Infrastructure.Random;
using UnityEngine;
using Random = System.Random;

namespace EnvironmentComponents
{
    public class LevelFloor
    {
        public WayPoint ShootngPos;
        public WayPoint InPoint;
        public WayPoint OutPoint;

        public LevelFloor(WayPoint inPoint, WayPoint outPoint,WayPoint shootngPos)
        {
            ShootngPos = shootngPos;
            InPoint = inPoint;
            OutPoint = outPoint;
        }

        public WayPoint GetStairConnectPosition(Vector3 connecting)
        {
            if (connecting.x > InPoint.Position.x)
            {
                return InPoint;   
            }
            return OutPoint;
        }
        
        public WayPoint GetShootingPosition() => ShootngPos;
        public Vector3 GetTeleportPosition() => ShootngPos.Position + ShootngPos.ForEnemyOffset;

        public WayPoint GetOutWayPoint(Vector3 mover)
        {
            if (mover.x > InPoint.Position.x)
            {
                return OutPoint;   
            }
            return InPoint;
        }

        public WayPoint GetInPoint(WayPoint previousPoint)
        {
            Vector3 nextPointPos = new Vector3(previousPoint.Position.x,InPoint.Position.y,previousPoint.Position.z);
            WayPoint next = new WayPoint(nextPointPos,InPoint.ForEnemyOffset);
            return next;
        }
        

        public Vector3 GetSpawnPosition() => InPoint.Position + InPoint.ForEnemyOffset;
    }
}