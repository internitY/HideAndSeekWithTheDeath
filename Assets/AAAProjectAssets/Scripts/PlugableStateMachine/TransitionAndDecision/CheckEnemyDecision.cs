using UnityEngine;

namespace MAED.ActionAndStates
{
    [CreateAssetMenu(menuName = "MAED/PlugableStateMachine/Decisions/CheckEnemyDecision")]
    public class CheckEnemyDecision : Decision
    {
        public override bool Decide(PlugableStateController controller)
        {
            bool targetVisible = CheckForEnemies(controller);
            return targetVisible;
        }

        private bool CheckForEnemies(PlugableStateController controller)
        {
            controller.nearCols = Physics.OverlapSphere(controller.transform.position, controller.VisionRadius, controller.EnemyMask, QueryTriggerInteraction.Ignore);

            if (controller.nearCols.Length <= 0)
            {
                //Debug.Log("No EnemyCollider found.");
                return false;
            }

            PlugableStateController target = null;

            if (controller.nearCols.Length > 0)
            {
                float sqrD = Mathf.Infinity;

                //get nearest enemy
                for (int i = 0; i < controller.nearCols.Length; i++)
                {
                    if (controller.nearCols[i].gameObject == controller.gameObject)
                        continue;

                    if (controller.nearCols[i].TryGetComponent(out PlugableStateController enemyController))
                    {
                        if (Physics.Linecast(controller.Eye.position, controller.nearCols[i].ClosestPoint(controller.Eye.position), out RaycastHit info,
                            controller.VisionBlockMask, QueryTriggerInteraction.Ignore))
                        {
                            Debug.Log("Enemy |" + enemyController.name + "| Vision blocked by " + info.transform.name);
                        }
                        else
                        {
                            float d = (controller.nearCols[i].transform.position - controller.transform.position).sqrMagnitude;

                            if (d < sqrD)
                            {
                                target = enemyController;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Found enemy col with no plugable state controller interface.");
                    }
                }

                if (target != null)
                {
                    controller.SetChaseTarget(target);
                    Debug.DrawLine(controller.Eye.position, target.transform.position, Color.yellow, 1f);
                    return true;
                }

                //Debug.Log(name + " CheckForEnemies returned: null");
                return false;
            }

            //Debug.Log(name + " CheckForEnemies returned: null");
            return false;
        }
    }
}

