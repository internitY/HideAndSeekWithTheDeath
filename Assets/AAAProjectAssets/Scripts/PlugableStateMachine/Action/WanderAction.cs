using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Wander")]
    public class WanderAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Wander(controller);
        }
        private void Wander(PlugableStateController controller)
        {
            if (controller.ReachedDestination)
            {
                controller.SetDestination(NextRandomWaypoint(controller));
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