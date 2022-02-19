using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/TakeClosestPatrolPoint")]
    public class TakeClosestPatrolPointEvent : StateEvent
    {
        public override void CallEvent(PlugableStateController controller)
        {
            DeathPlugableStateController deathController = controller as DeathPlugableStateController;

            if (deathController == null)
            {
                Debug.LogWarning("Patrol state not allowed for non-deathplugtablestatecontroller yet.");
                return;
            }

            deathController.TakeNextPatrolPoint(deathController.GetClosestPatrolWaypointIndex(controller.transform.position));
        }
    }
}

