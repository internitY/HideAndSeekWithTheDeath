using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace MAED.ActionAndStates
{
    public class PlugableStateController : MonoBehaviour
    {
        private RichAI aiPath;
        private Seeker seeker;
        private RVOController rvo;
        private Animator anim;

        [SerializeField] private bool aiIsActive = true;
        [SerializeField] private bool blockWhilePathCalculating = true;

        [Header("State")]
        [SerializeField] private State currentState;
        [SerializeField] private State chaseState;
        [SerializeField] private State interactState;
        [SerializeField] private State resetstate;
        [SerializeField] private State remainState;

        [HideInInspector] public Collider[] nearCols;

        [Header("Entity Values")] 
        [SerializeField, Range(5f, 30f)] private float visionRadius = 10f;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private LayerMask visionBlockMask;
        [SerializeField] private Transform eye;
        [SerializeField, ShowOnly] protected bool isDead = false;

        [Header("Destination")]
        [SerializeField, ShowOnly] protected float magnitude;
        private Path path;

        [Header("Target References")] 
        [SerializeField, ShowOnly] protected PlugableStateController chaseTarget;

        [Header("Timers")] 
        [ShowOnly] public float eventTime;

        [Header("Debug")] 
        [SerializeField] private bool enableDebug = false;

        #region getter

        public bool IsDead
        {
            get => isDead;
            set => isDead = value;
        }
        public PlugableStateController ChaseTarget;
        public float VisionRadius => visionRadius;
        public LayerMask EnemyMask => enemyMask;
        public LayerMask VisionBlockMask => visionBlockMask;
        public Transform Eye => eye;
        #endregion getter

        #region unity
        private void Awake()
        {
            aiPath = GetComponent<RichAI>();
            seeker = GetComponent<Seeker>();
            rvo = GetComponent<RVOController>();
            anim = GetComponentInChildren<Animator>();
        }
        private void Start()
        {
            SetToState(resetstate);
            aiIsActive = true;
        }
        protected virtual void OnEnable()
        {
            
        }
        protected virtual void OnDisable()
        {

        }

        protected virtual void Update()
        {
            UpdateState();
        }
        #endregion unity

        #region state controller
        public void UpdateState()
        {
            if (!aiIsActive || isDead)
                return;

            magnitude = ReachedDestination ? 0f : aiPath.velocity.magnitude;
            anim?.SetFloat("sqrVelocity", magnitude * 0.5f);
            currentState.UpdateState(this);
        }
        /// <summary>
        /// Sets the state to the specific state.
        /// Usually this should only be done for the given states of this controller (e.g. controller.ChaseState).
        /// </summary>
        public void SetToState(State nextState)
        {
            if (nextState == remainState || nextState == null)
            {
                #if UNITY_EDITOR
                if (enableDebug)
                {
                    Debug.LogWarning(name + " SetToState returned.");
                }
                #endif
                return;
            }

            #if UNITY_EDITOR
            if (enableDebug)
            {
                Debug.Log(name + " SetToState to " + nextState);
            }
            #endif

            if(currentState != null)
                currentState.OnStateExit(this);

            currentState = nextState;

            currentState.OnStateEnter(this);
        }
        /// <summary>
        /// Checks if the event time is reached.
        /// Does automatically add delta time each time it is requested.
        /// </summary>
        public bool CheckIfEventTimeElapsed(float duration)
        {
            eventTime += Time.deltaTime;
            return eventTime >= duration;
        }
        /// <summary>
        /// Resets the event timer to 0f;
        /// </summary>
        public void ResetEventTime()
        {
            eventTime = 0.0f;
        }
        #endregion

        #region pathfinding
        public bool PathIsPossible(Vector3 queryPosition, Vector3 destination)
        {
            var startNode = AstarPath.active.GetNearest(queryPosition, NNConstraint.Default).node;
            var endNode = AstarPath.active.GetNearest(destination, NNConstraint.Default).node;
            return PathUtilities.IsPathPossible(startNode, endNode);
        }
        public bool SetDestination(Vector3 destination, bool checkForValidPath = false)
        {
            if (checkForValidPath)
            {
                if (!PathIsPossible(transform.position, destination))
                {
                    #if UNITY_EDITOR
                    if (enableDebug)
                    {
                        Debug.LogWarning(name + " SetDestination to " + destination + " false. Path is not possible.");
                    }
                    #endif

                    return false;
                }
            }
            
            aiPath.canMove = true;
            aiPath.rvoDensityBehavior.ClearDestinationReached();
            path = seeker.StartPath(transform.position, destination);

            //to be deterministic, path calculation needs to be blocked to stop async path calculation
            if (blockWhilePathCalculating)
            {
                path.BlockUntilCalculated();
            }

            #if UNITY_EDITOR
            if (enableDebug)
            {
                Debug.Log(name + " SetDestination to " + destination);
            }
            #endif

            return true;
        }
        public bool StopMovement()
        {
            aiPath.canMove = false;
            seeker.CancelCurrentPathRequest();
            anim?.SetFloat("sqrVelocity", 0f);
            return true;
        }
        public bool ReachedDestination
        {
            get
            {
                bool reachedDestination = !aiPath.pathPending 
                                          && (aiPath.reachedEndOfPath || aiPath.reachedDestination || !aiPath.hasPath || aiPath.rvoDensityBehavior.reachedDestination);

                #if UNITY_EDITOR
                if (enableDebug)
                {
                    //Debug.Log(name + " reached destination = " + reachedDestination);
                }
                #endif

                return reachedDestination;
            }
        }
        #endregion pathfinding

        public void SetChaseTarget(PlugableStateController target)
        {
            chaseTarget = target;

            if (target != null)
            {
                SetDestination(target.transform.position);
            }
        }

#if UNITY_EDITOR
        #region gizmos
        private void OnDrawGizmos()
        {
            if (currentState != null)
            {
                Gizmos.color = currentState.SceneGizmoColor;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.3f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!enableDebug)
                return;
            
        }
        #endregion gizmos
#endif
    }
}