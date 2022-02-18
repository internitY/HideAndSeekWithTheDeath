using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/SetControllerMoveSpeed")]
    public class SetControllerMoveSpeedEvent : StateEvent
    {
        [SerializeField] private float moveSpeed = 4f;
        public override void CallEvent(PlugableStateController controller)
        {
            controller.RichAI.maxSpeed = moveSpeed;
        }
    }
}

