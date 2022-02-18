using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Chase(controller);
        }
        private void Chase(PlugableStateController controller)
        {
            //check for repathing
            if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.transform.position) > controller.VisionRadius)
            {
                controller.SetDestination(controller.ChaseTarget.transform.position);
            }
            else
            {
                controller.StopMovement();
            }
        }
    }
}
