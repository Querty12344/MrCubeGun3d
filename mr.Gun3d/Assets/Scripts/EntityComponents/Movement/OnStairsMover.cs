using System;
using System.Collections;
using EnvironmentComponents;
using Infrastructure.Constants;
using ModestTree;
using UnityEngine;

namespace EntityComponents.Movement
{
    public class OnStairsMover
    {
        private IEntityMover _mover;
        private EntitySettings _settings;
        private IEntityAnimator _animator;

        public OnStairsMover(IEntityMover mover, EntitySettings settings, IEntityAnimator animator)
        {
            _mover = mover;
            _settings = settings;
            _animator = animator;
        }
        

        public IEnumerator Moving(Rigidbody thisBody,WayPoint[] activeWay,Action movingEnded)
        {
            WayPoint activeWayPoint = activeWay[0];
            while (_mover.IsMoving)
            {
                MoveToDot(thisBody,activeWayPoint);
                if (IsOnTarget(activeWayPoint.Position,thisBody.position))
                {
                    var activePointIndex = activeWay.IndexOf(activeWayPoint);
                    if (activePointIndex == activeWay.Length - 1)
                    {
                        _mover.IsMoving = false;
                        _animator.OnWaiting();
                        if (!_mover.IsRunning)
                        {
                            movingEnded.Invoke();   
                        }
                        yield break;
                    }

                    activeWayPoint = activeWay[activePointIndex + 1];
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private void MoveToDot(Rigidbody thisBody,WayPoint activeWayPoint)
        {
            Vector3 moveVector = (activeWayPoint.Position + activeWayPoint.ForEnemyOffset - thisBody.transform.position).normalized;
            Vector3 nextPosition = thisBody.transform.position + moveVector * _settings.EntitiesMovingSpeed;
            thisBody.MovePosition(nextPosition);
        }
        

        private bool IsOnTarget(Vector3 dot,Vector3 pos)
        {
            return Math.Abs(dot.x - pos.x) <= _settings.OnPlaceDistance;
        }
    }
}