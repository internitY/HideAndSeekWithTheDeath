using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/TargetIsHiding")]
    public class TargetIsHidingDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            return TargetIsHiding(controller);
        }

        private bool TargetIsHiding(PlugableStateController controller)
        {
            if (controller.ChaseTarget.IsHiding)
            {
                return true;
            }

            return false;
        }
    }
}
