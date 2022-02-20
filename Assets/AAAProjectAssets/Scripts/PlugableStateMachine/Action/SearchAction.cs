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
                DeathPlugableStateController death = controller as DeathPlugableStateController;

                if (death == null)
                {
                    Debug.LogWarning("Search Action handles by a non valid child class of plugable state controller. (not death).");
                }
                else
                {
                    if (death.DeathPatrolType == DeathPlugableStateController.DeathType.Patroler)
                    {
                        death.SetToState(death.PatrolState);
                    }
                    else
                    {
                        death.SetToState(death.ResetState);
                    }
                }
                
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
