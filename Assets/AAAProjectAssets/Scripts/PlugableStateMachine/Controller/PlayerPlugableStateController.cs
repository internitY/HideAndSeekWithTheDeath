using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAED.ActionAndStates
{
    public class PlayerPlugableStateController : PlugableStateController
    {
        public override void OnUpdateState()
        {
            anim?.SetFloat("velocity", magnitude);
        }

        public override void OnMovementStopped()
        {
            anim?.SetFloat("velocity", 0f);
        }
    }
}


