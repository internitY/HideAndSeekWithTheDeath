using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Interact")]
    public class InteractAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            Interact(controller);
        }
        private void Interact(PlugableStateController controller)
        {
            /*
            bool reachedInteractable = controller.ReachedDestination || controller.Interactable.UnitIsInInteractionRange(controller.transform.position);
            if (reachedInteractable)
            {
                if (controller.Interactable.InteractionAllowed(controller))
                {
                    if (controller.CheckIfEventTimeElapsed(controller.Interactable.UnitInteractDuration))
                    {
                        controller.Interactable.Interact(controller);
                    }
                }
            }
            */
        }
    }
}

