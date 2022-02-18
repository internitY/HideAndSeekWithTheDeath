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

            if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.transform.position) > controller.VisionRadius)
            {
                return true;
            }

            if (Physics.Linecast(controller.Eye.position, controller.ChaseTarget.Eye.position,
                controller.VisionBlockMask, QueryTriggerInteraction.Ignore))
            {
                return true;
            }

            return false;
        }
    }
}
