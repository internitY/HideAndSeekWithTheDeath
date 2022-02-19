using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Actions/Search")]
    public class SearchAction : Action
    {
        [SerializeField] private float maxSearchTime = 3f;
        [SerializeField] private float searchTargetDirectionLength = 2f;
        public override void Act(PlugableStateController controller)
        {
            Search(controller);
        }

        private void Search(PlugableStateController controller)
        {
            if (controller.CheckIfEventTimeElapsed(maxSearchTime))
            {
                controller.SetToState(controller.ResetState);
                return;
            }

            if (controller.ReachedDestination)
            {
                Vector3 newSearchPosition = controller.transform.position + controller.LastTargetDirection * searchTargetDirectionLength;
                controller.SetDestination(newSearchPosition);
            }
        }
    }
}
