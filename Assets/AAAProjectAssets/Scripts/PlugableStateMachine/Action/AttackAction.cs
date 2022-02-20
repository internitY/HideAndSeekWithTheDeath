using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Attack(controller);
        }

        private void Attack(PlugableStateController controller)
        {
            if (controller.CheckIfEventTimeElapsed(1.5f))
            {
                //check for repathing
                if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.transform.position) < controller.AttackRadius)
                {
                    controller.ChaseTarget.TakeDamage(controller);
                    controller.ResetEventTime();
                }
            }
        }
    }
}

