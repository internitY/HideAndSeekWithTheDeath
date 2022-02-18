using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAED.ActionAndStates
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(PlugableStateController controller);
    }
}

