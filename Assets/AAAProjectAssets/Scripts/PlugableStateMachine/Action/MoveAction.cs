using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Move")]
    public class MoveAction : Action
    {
        public override void Act(PlugableStateController controller)
        {
            if (!controller.ReachedDestination)
            {

            }
        }
    }
}
