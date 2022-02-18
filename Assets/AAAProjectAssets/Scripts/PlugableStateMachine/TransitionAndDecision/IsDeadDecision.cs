using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/IsDeadDecision")]
    public class IsDeadDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            return controller.IsDead;
        }
    }
}
