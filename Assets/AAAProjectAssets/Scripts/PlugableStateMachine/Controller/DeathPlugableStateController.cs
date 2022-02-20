using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace MAED.ActionAndStates
{
    public class DeathPlugableStateController : PlugableStateController
    {
        [Header("Death Patrol")] 
        [SerializeField] private State patrolState;
        [SerializeField] private DeathType deathPatrolType = DeathType.Randomer;
        public enum DeathType
        {
            Randomer,
            Patroler
        }

        [SerializeField] private PatrolPath currentPatrolPath;
        [SerializeField, ShowOnly] private Transform nextPatrolPoint = null;
        [SerializeField, ShowOnly] private int currentPatrolPointIndex = 0;
        [SerializeField, ShowOnly] private bool patrolForward = true;

        [Header("Death UI")] 
        [SerializeField] private Image eyeImage;
        [SerializeField] private Color wanderColor = Color.yellow;
        [SerializeField] private Color chaseColor = Color.red;

        [Header("VfX")] 
        [SerializeField] private VisualEffect SpawnEffect;

        public State PatrolState => patrolState;

        public DeathType DeathPatrolType
        {
            get => deathPatrolType;
            set
            {
                deathPatrolType = value;
                if (deathPatrolType == DeathType.Patroler)
                {
                    SetToState(PatrolState);
                }
                else
                {
                    SetToState(ResetState);
                }
            }
        }

        protected override IEnumerator Start()
        {
            if (SpawnEffect != null)
            {
                SpawnEffect.gameObject.SetActive(true);
            }

            while (PatrolPathManager.Instance == null)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            if (SpawnEffect != null)
            {
                SpawnEffect.gameObject.SetActive(false);
            }

            if (deathPatrolType == DeathType.Patroler)
            {
                if (currentPatrolPath == null)
                {
                    TakeClosestPatrolPath();
                }
                else
                {
                    currentPatrolPath.TakePatrolPath(this);
                    SetToState(PatrolState);
                }
            }
            else
            {
                SetToState(ResetState);
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            eyeImage.color = wanderColor;
            MasterAudio.PlaySound3DFollowTransform("DeathWalk", transform);
        }
        public override void OnChaseTargetSet(PlugableStateController target)
        {
            eyeImage.color = CurrentState.SceneGizmoColor;
        }

        #region patrol
        public void TakeNextPatrolPoint(int overwriteIndex = -1)
        {
            if (overwriteIndex >= 0)
            {
                currentPatrolPointIndex = Mathf.Clamp(overwriteIndex, 0, currentPatrolPath.PathPoints.Length - 1);
            }
            else
            {
                currentPatrolPointIndex = patrolForward ? currentPatrolPointIndex + 1 : currentPatrolPointIndex - 1;
            }

            if (currentPatrolPointIndex > currentPatrolPath.PathPoints.Length - 1)
            {
                switch (currentPatrolPath.PathType)
                {
                    case PatrolPath.PatrolPathType.Loop:
                        currentPatrolPointIndex = 0;
                        break;
                    case PatrolPath.PatrolPathType.UpAndDown:
                        patrolForward = !patrolForward;
                        currentPatrolPointIndex = currentPatrolPath.PathPoints.Length - 1;
                        break;
                }
            }

            if (currentPatrolPointIndex < 0)
            {
                switch (currentPatrolPath.PathType)
                {
                    case PatrolPath.PatrolPathType.Loop:
                        currentPatrolPointIndex = currentPatrolPath.PathPoints.Length - 1;
                        break;
                    case PatrolPath.PatrolPathType.UpAndDown:
                        patrolForward = !patrolForward;
                        currentPatrolPointIndex = 0;
                        break;
                }
            }

            nextPatrolPoint = currentPatrolPath.PathPoints[currentPatrolPointIndex];
            SetDestination(nextPatrolPoint.position);
            //Debug.Log(name + " took next patrol point to " + patrolWaypoints[currentPatrolPointIndex]);
        }
        public void TakeClosestPatrolPathPoint()
        {
            TakeNextPatrolPoint(currentPatrolPath.GetClosestPatrolWaypointIndex(transform.position));
        }
        public void TakeClosestPatrolPath(bool mustBeUnreserved = true)
        {
            PatrolPath path = PatrolPathManager.Instance.GetClosestPatrolPath(transform.position, mustBeUnreserved);

            if (path != null)
            {
                currentPatrolPath = path;

                if (mustBeUnreserved)
                    currentPatrolPath.TakePatrolPath(this);

                SetToState(PatrolState);
                deathPatrolType = DeathType.Patroler;
            }
            else
            {
                Debug.LogWarning("PatrolPath is null, also already reserved or nothing found. Death [" + name + "] falling back to randomer type.");

                SetToState(ResetState);
                deathPatrolType = DeathType.Randomer;
            }
        }
        #endregion patrol

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if (currentPatrolPath != null && currentPatrolPath.PathPoints.Length > 0)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, currentPatrolPath.PathPoints[currentPatrolPath.GetClosestPatrolWaypointIndex(transform.position)].position);
            }
        }
    }
}

