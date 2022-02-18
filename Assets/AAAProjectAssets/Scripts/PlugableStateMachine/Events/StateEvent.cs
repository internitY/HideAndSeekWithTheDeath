using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAED.ActionAndStates
{
    public abstract class StateEvent : ScriptableObject
    {
        public abstract void CallEvent(PlugableStateController controller);
    }
}

