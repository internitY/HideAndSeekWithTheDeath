using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/InteractableIsTarget")]
    public class InteractableIsTargetDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            /*
            bool interactableIsTarget = controller.Interactable != null && controller.Interactable.gameObject.activeSelf;
            return interactableIsTarget;
            */
            return true;
        }
    }
}

