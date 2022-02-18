using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/ClearChaseTarget")]
    public class ClearChaseTargetEvent : StateEvent
    {
        public override void CallEvent(PlugableStateController controller)
        {
            controller.SetChaseTarget(null);
        }
    }
}
