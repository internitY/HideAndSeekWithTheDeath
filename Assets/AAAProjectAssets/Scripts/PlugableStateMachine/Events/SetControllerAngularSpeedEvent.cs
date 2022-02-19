using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/SetControllerAngularSpeed")]
    public class SetControllerAngularSpeedEvent : StateEvent
    {
        [SerializeField, Range(45f, 1080f)] private float angularSpeed = 120f;
        public override void CallEvent(PlugableStateController controller)
        {
            controller.RichAI.rotationSpeed = angularSpeed;

        }
    }
}

