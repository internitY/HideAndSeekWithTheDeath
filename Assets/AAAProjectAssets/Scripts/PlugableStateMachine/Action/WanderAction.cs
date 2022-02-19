using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Wander")]
    public class WanderAction : Action
    {
        [SerializeField] private float nextPointDelay = 1f;
        public override void Act(PlugableStateController controller)
        {
            Wander(controller);
        }
        private void Wander(PlugableStateController controller)
        {
            if (controller.ReachedDestination)
            {
                if (controller.CheckIfEventTimeElapsed(nextPointDelay))
                {
                    controller.SetDestination(NextRandomWaypoint(controller));
                    controller.ResetEventTime();
                }
            }
        }
        private Vector3 NextRandomWaypoint(PlugableStateController controller)
        {
            Vector3 newPos = controller.transform.position + Random.insideUnitSphere * controller.VisionRadius;
            newPos.y = controller.transform.position.y;
            return newPos;
        }
    }
}