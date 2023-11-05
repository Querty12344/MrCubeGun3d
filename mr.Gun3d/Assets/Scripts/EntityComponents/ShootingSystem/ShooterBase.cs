using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public abstract class ShooterBase
    {
        protected Vector3 CalculateRightDirection(Transform shooter, Transform target, float aimOffset)
        {
            var yAimOffset = new Vector3(0, aimOffset, 0);
            Vector3 rightVector =  (target.position + yAimOffset - shooter.position).normalized;
            rightVector.z = 0f;
            return rightVector;
        }
    }
}