using System;
using UnityEngine;
using UnityEngine.UI;

namespace MAED.ActionAndStates
{
    public class DeathPlugableStateController : PlugableStateController
    {
        [Header("Death Patrol")] 
        [SerializeField] private PatrolType patrolType = PatrolType.Random;
        [SerializeField] private Transform[] patrolWaypoints;
        [SerializeField, ShowOnly] private Transform nextPatrolPoint = null;
        [SerializeField, ShowOnly] private int currentPatrolPointIndex = 0;
        [SerializeField, ShowOnly] private bool patrolForward = true;

        private enum PatrolType
        {
            Random,
            Loop,
            UpAndDown
        }

        [Header("Death UI")] 
        [SerializeField] private Image eyeImage;
        [SerializeField] private Color wanderColor = Color.yellow;
        [SerializeField] private Color chaseColor = Color.red;

        protected override void OnEnable()
        {
            base.OnEnable();
            eyeImage.color = wanderColor;
        }

        public override void OnChaseTargetSet(PlugableStateController target)
        {
            eyeImage.color = CurrentState.SceneGizmoColor;
        }

        #region patrol
        public int GetClosestPatrolWaypointIndex(Vector3 queryPosition)
        {
            float maxDist = Mathf.Infinity;
            int result = 0;

            for (int i = 0; i < patrolWaypoints.Length; i++)
            {
                float dist = (queryPosition - transform.position).sqrMagnitude;

                if (dist < maxDist)
                {
                    maxDist = dist;
                    result = i;
                }
            }

            return result;
        }

        public void TakeNextPatrolPoint(int overwriteIndex = -1)
        {
            if (overwriteIndex >= 0)
            {
                nextPatrolPoint = patrolWaypoints[Mathf.Clamp(overwriteIndex, 0, patrolWaypoints.Length -1)];
                SetDestination(nextPatrolPoint.position);
                Debug.Log(name + " took next patrol point to " + patrolWaypoints[currentPatrolPointIndex] + " by overwriting.");
            }

            currentPatrolPointIndex = patrolForward ? currentPatrolPointIndex + 1 : currentPatrolPointIndex - 1;

            if (currentPatrolPointIndex > patrolWaypoints.Length - 1)
            {
                switch (patrolType)
                {
                    case PatrolType.Loop:
                        currentPatrolPointIndex = 0;
                        break;
                    case PatrolType.UpAndDown:
                        patrolForward = !patrolForward;
                        currentPatrolPointIndex = patrolWaypoints.Length - 1;
                        break;
                }
            }

            if (currentPatrolPointIndex < 0)
            {
                switch (patrolType)
                {
                    case PatrolType.Loop:
                        currentPatrolPointIndex = patrolWaypoints.Length - 1;
                        break;
                    case PatrolType.UpAndDown:
                        patrolForward = !patrolForward;
                        currentPatrolPointIndex = 0;
                        break;
                }
            }

            nextPatrolPoint = patrolWaypoints[currentPatrolPointIndex];
            SetDestination(nextPatrolPoint.position);
            //Debug.Log(name + " took next patrol point to " + patrolWaypoints[currentPatrolPointIndex]);
        }
        #endregion patrol

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (patrolWaypoints.Length > 0 && patrolType != PatrolType.Random)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, patrolWaypoints[0].position);

                for (int i = 0; i < patrolWaypoints.Length - 1; i++)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(transform.position, patrolWaypoints[i + 1].position);

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(patrolWaypoints[i].position, patrolWaypoints[i + 1].position);
                }

                if (patrolType == PatrolType.Loop)
                {
                    Gizmos.DrawLine(patrolWaypoints[0].position, patrolWaypoints[patrolWaypoints.Length - 1].position);
                }
            }
        }
    }
}

