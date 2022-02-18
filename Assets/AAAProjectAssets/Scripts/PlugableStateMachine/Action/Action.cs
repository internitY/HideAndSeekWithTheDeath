using UnityEngine;

namespace MAED.ActionAndStates
{
    public abstract class Action : ScriptableObject
    {
        
        /// <summary>
        /// Is getting called per frame to calculate the Action.
        /// </summary>
        public abstract void Act(PlugableStateController controller);
    }
}