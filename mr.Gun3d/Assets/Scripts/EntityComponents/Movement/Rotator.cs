using System;
using System.Collections;
using UnityEngine;

namespace EntityComponents.Movement
{
    public class Rotator
    {
        public IEnumerator Rotating(Vector3 targetPosition,Transform transform,float speed)
        {
            while (true)
            {
                if (transform.position.x > targetPosition.x)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), speed);
                    if (Math.Abs(Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 180, 0))) < 4)
                        yield break;
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), speed);
                    if (Math.Abs(Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, 0))) < 4) 
                        yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}