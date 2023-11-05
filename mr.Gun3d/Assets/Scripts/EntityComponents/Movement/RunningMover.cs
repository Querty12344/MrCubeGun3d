using System.Collections;
using Infrastructure.Constants;
using UnityEngine;

namespace EntityComponents.Movement
{
    public class RunningMover
    {
        private readonly EntitySettings _settings;
        private EntityMover _mover;
        private Jumper _jumper;

        public RunningMover(EntitySettings settings, Jumper jumper, EntityMover mover)
        {
            _settings = settings;
            _jumper = jumper;
            _mover = mover;
        }

        public IEnumerator Run(float zOffset,bool isHunter,int rotationCof,Rigidbody thisBody,Transform enemy)
        {
            float startZOffset = zOffset;
            float finalZpos = thisBody.position.z - zOffset;
            while (_mover.IsRunning)
            {
                MoveForward(zOffset,isHunter,rotationCof,thisBody,enemy,finalZpos);
                if (Mathf.Abs(zOffset) > 0.01f)
                {
                    zOffset -= startZOffset * _settings.RunSpeed;
                }
                else
                {
                    zOffset = 0f;
                }
                yield return new WaitForFixedUpdate();
            }
            
        }
        private void MoveForward(float zOffset,bool isHunter,int rotationCof,Rigidbody thisBody,Transform enemy,float finalZpos)
        {
            if (thisBody == null || enemy == null)
            {
                return;
            }
            float speedCof = 1f;
            if (isHunter)
            {
                speedCof = Mathf.Abs(thisBody.transform.position.x - enemy.position.x)/_settings.ToEnemyDistance ;
            }
            else
            {
                speedCof = _settings.ToEnemyDistance / Mathf.Abs(thisBody.transform.position.x - enemy.position.x);
            }
            
            Vector3 horizontalOffset = Vector3.left*rotationCof*_settings.RunSpeed * speedCof -  Vector3.forward*zOffset*_settings.RunSpeed ;
            Vector3 verticalOffset  = _jumper.GetJumpOffset();
            Vector3 finalPos = thisBody.transform.position + horizontalOffset + verticalOffset;
            if (zOffset == 0f)
            {
                finalPos.z = finalZpos;
            }
            thisBody.MovePosition(finalPos);
        }
    }
}