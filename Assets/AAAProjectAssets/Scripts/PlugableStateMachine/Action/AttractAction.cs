using UnityEngine;

namespace MAED.ActionAndStates
{
    public class AttractAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Attract(controller);
        }

        private void Attract(PlugableStateController controller)
        {
            //check for repathing
            if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.transform.position) < controller.VisionRadius)
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

