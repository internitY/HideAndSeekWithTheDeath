using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Patrol(controller);
        }
        private void Patrol(PlugableStateController controller)
        {
            /*
            if (controller.ReachedDestination)
            {
                //check if ai follows path forward or backwards
                if (controller.moveForward)
                {
                    controller.nextWaypoint++;

                    if (controller.nextWaypoint > controller.waypoints.Count - 1)
                    {
                        controller.moveForward = false;
                    }
                }
                else
                {
                    controller.nextWaypoint--;

                    if (controller.nextWaypoint <= 0)
                    {
                        controller.moveForward = true;
                    }
                }

                controller.SetDestination(controller.waypoints[controller.nextWaypoint]);
            }
             */
        }
    }
}
