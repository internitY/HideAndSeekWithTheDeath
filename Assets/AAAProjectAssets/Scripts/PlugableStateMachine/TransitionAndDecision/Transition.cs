using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAED.ActionAndStates
{
    [System.Serializable]
    public class Transition
    {
        public Decision Decision;
        public State TrueState;
        public State FalseState;
    }
}