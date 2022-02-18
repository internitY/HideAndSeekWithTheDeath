using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/ResetControllerTimer")]
    public class ResetControllerTimerEvent : StateEvent
    {
        public override void CallEvent(PlugableStateController controller)
        {
            controller.ResetEventTime();
        }
    }
}

