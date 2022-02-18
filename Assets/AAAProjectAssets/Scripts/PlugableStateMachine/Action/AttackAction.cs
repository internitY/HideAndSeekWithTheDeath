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
            if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.transform.position) < 2f)
            {
                controller.ChaseTarget.IsDead = true;
            }
        }
    }
}