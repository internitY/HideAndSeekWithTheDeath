using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/DestinationReachedDecision")]
    public class DestinationReachedDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            bool reached = DestinationReached(controller);
            return reached;
        }

        private bool DestinationReached(PlugableStateController controller)
        {
            return controller.ReachedDestination;
        }
    }
}
