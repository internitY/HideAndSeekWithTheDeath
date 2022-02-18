using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu (menuName = "MAED/PlugableStateMachine/States/NewState")]
    public class State : ScriptableObject
    {
        public StateEvent[] StartEnterEvents;
        public Action[] Actions;
        public Transition[] Transitions;
        public StateEvent[] StartExitEvents;
        public Color SceneGizmoColor = Color.grey;

        public virtual void OnStateEnter(PlugableStateController controller)
        {
            for (int i = 0; i < StartEnterEvents.Length; i++)
            {
                StartEnterEvents[i].CallEvent(controller);
            }
        }
        public void UpdateState(PlugableStateController controller, bool updateAction = true, bool updateTransition = true)
        {
            if (updateAction)
                DoActions(controller);

            if (updateTransition)
                CheckTransitions(controller);
        }
        public virtual void OnStateExit(PlugableStateController controller)
        {
            for (int i = 0; i < StartExitEvents.Length; i++)
            {
                StartExitEvents[i].CallEvent(controller);
            }
        }
        private void DoActions(PlugableStateController controller)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                Actions[i].Act(controller);
            }
        }
        private void CheckTransitions(PlugableStateController controller)
        {
            for (int i = 0; i < Transitions.Length; i++)
            {
                bool transitionSucceeded = Transitions[i].Decision.Decide(controller);

                if (transitionSucceeded)
                {
                    controller.SetToState(Transitions[i].TrueState);
                }
                else
                {
                    controller.SetToState(Transitions[i].FalseState);
                }
            }
        }
    }
}