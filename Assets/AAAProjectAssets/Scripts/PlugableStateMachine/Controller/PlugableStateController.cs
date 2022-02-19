using System.Collections;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace MAED.ActionAndStates
{
    public class PlugableStateController : MonoBehaviour
    {
        private RichAI aiPath;
        private Seeker seeker;
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
        [SerializeField, Range(1f, 5f)] private float attackRadius = 2f;
        [SerializeField, Range(1f, 30f)] private float visionRadius = 10f;
        [SerializeField, Range(10f, 360f)] private float visionAngle = 360f;
        [SerializeField, Range(1f, 10f)] private float overallAttractRadius = 3f;

        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private LayerMask visionBlockMask;
        [SerializeField] private Transform eye;
        [SerializeField, ShowOnly] protected bool isDead = false;
        [SerializeField, ShowOnly] protected bool isHiding = false;

        [Header("Destination")]
        [SerializeField, ShowOnly] protected float magnitude;
        private Path path;

        [Header("Target References")]
        [SerializeField, ShowOnly] protected PlugableStateController chaseTarget;
        [SerializeField, ShowOnly] protected Vector3 lastTargetPosition;
        [SerializeField, ShowOnly] protected Vector3 lastTargetDirection;

        [Header("Timers")] 
        [ShowOnly] public float eventTime;

        [Header("Debug")] 
        [SerializeField] private bool enableDebug = false;

        #region getter

        public bool IsActive
        {
            get => aiIsActive;
            set
            {
                aiIsActive = value;

                if (!IsActive)
                    StopMovement();
            }
        }
        public bool IsDead
        {
            get => isDead;
            set
            {
                isDead = value;
            }
        }
        public bool IsHiding
        {
            get => isHiding;
            set
            {
                isHiding = value;
            }
        }
        public RichAI RichAI => aiPath;
        public State CurrentState => currentState;
        public State ResetState => resetstate;
        public PlugableStateController ChaseTarget => chaseTarget;
        public Vector3 LastTargetPosition { 
            get => lastTargetPosition;
            set => lastTargetPosition = value;
        }
        public Vector3 LastTargetDirection
        {
            get => lastTargetDirection;
            set => lastTargetDirection = value;
        }
        public float AttackRadius => attackRadius;
        public float VisionRadius => visionRadius;
        public float OverallAttractRadius => overallAttractRadius;
        public LayerMask EnemyMask => enemyMask;
        public LayerMask VisionBlockMask => visionBlockMask;
        public Transform Eye => eye;
        #endregion getter

        #region unity
        private void Awake()
        {
            aiPath = GetComponent<RichAI>();
            seeker = GetComponent<Seeker>();
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
            anim?.SetFloat("velocity", magnitude);
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
                    //Debug.LogWarning(name + " SetToState returned.");
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

            if (currentState != null)
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
            aiPath.destination = transform.position;
            aiPath.canMove = false;
            seeker.CancelCurrentPathRequest();
            anim?.SetFloat("velocity", 0f);
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

        #region target chasing
        public bool TargetIsInsideVisionAngle(Vector3 queryPosition, bool igoreY = true)
        {
            if (visionAngle <= 0f || visionAngle >= 360f)
                return true;

            if (igoreY)
                queryPosition.y = 0;

            Vector3 targetVector = (queryPosition - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, targetVector);
            return angle <= visionAngle * 0.5f;
        }
        public void SetChaseTarget(PlugableStateController target)
        {
            chaseTarget = target;
            OnChaseTargetSet(chaseTarget);

            if (enableDebug)
            {
                if (chaseTarget == null)
                    Debug.Log(name + " set chase target to null.");
                else
                    Debug.Log(name + " set chase target to " + target.name);
            }
        }
        public virtual void OnChaseTargetSet(PlugableStateController target)
        {

        }

        public void TakeDamage(PlugableStateController attacker)
        {
            Debug.Log(name + " get attacked by " + attacker.name);
        }
        #endregion target chasing


#if UNITY_EDITOR
        #region gizmos
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(eye.position, visionRadius);

            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(eye.position, overallAttractRadius);

            if (currentState != null)
            {
                Gizmos.color = currentState.SceneGizmoColor;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.3f);
            }
            
            if (chaseTarget != null)
            {
                Gizmos.color = TargetIsInsideVisionAngle(chaseTarget.transform.position) ? Color.red : Color.yellow;
                Gizmos.DrawLine(eye.position, chaseTarget.Eye.position);
            }

            if (visionAngle < 360f)
            {
                Gizmos.color = Color.yellow;
                Vector3 leftAngle = Quaternion.AngleAxis(-visionAngle * 0.5f, Vector3.up) * transform.forward;
                Vector3 rightAngle = Quaternion.AngleAxis(visionAngle * 0.5f, Vector3.up) * transform.forward;
                Gizmos.DrawRay(eye.position, leftAngle * visionRadius);
                Gizmos.DrawRay(eye.position, rightAngle * visionRadius);
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (!enableDebug)
                return;
            
        }
        #endregion gizmos
#endif
    }
}