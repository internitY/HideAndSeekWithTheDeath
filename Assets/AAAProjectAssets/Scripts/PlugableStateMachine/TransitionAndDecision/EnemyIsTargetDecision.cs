using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/EnemyIsTarget")]
    public class EnemyIsTargetDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            bool targetFound = controller.ChaseTarget != null;
            return targetFound;
        }
    }
}
