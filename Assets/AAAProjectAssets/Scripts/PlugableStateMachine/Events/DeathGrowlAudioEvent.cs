using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/StateEvents/DeathGrowlAudio")]
    public class DeathGrowlAudioEvent : StateEvent
    {
        public override void CallEvent(PlugableStateController controller)
        {
            MasterAudio.PlaySound("DeathGrowl");
        }
    }
}
