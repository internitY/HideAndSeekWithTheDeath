using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Patrol")]
    public class PatrolAction : Action
    {
        [SerializeField] private float nextPatrolPointDelay = 2f;
        public override void Act(PlugableStateController controller)
        {
            Patrol(controller);
        }
        private void Patrol(PlugableStateController controller)
        {
            if (controller.ReachedDestination)
            {
                DeathPlugableStateController deathController = controller as DeathPlugableStateController;
                if (deathController != null)
                {
                    if (controller.CheckIfEventTimeElapsed(nextPatrolPointDelay))
                    {
                        //check if ai follows path forward or backwards
                        deathController.TakeNextPatrolPoint();
                        deathController.ResetEventTime();
                    }
                }
                else
                {
                    Debug.LogWarning("Patrolling not allowed for non-deathplugablestatecontrollers yet.");
                }
            }
        }
    }
}
