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
                //get nearest enemy
                for (int i = 0; i < controller.nearCols.Length; i++)
                {
                    if (controller.nearCols[i].gameObject == controller.gameObject)
                        continue;

                    if (controller.nearCols[i].TryGetComponent(out PlugableStateController enemyController))
                    {
                        if (Vector3.Distance(controller.transform.position, enemyController.transform.position) > controller.VisionRadius - enemyController.RichAI.radius)
                        {
                            continue;
                        }

                        if (Physics.Linecast(controller.Eye.position, enemyController.Eye.position, out RaycastHit info,
                            controller.VisionBlockMask, QueryTriggerInteraction.Ignore))
                        {
                            //Debug.Log("Enemy |" + enemyController.name + "| Vision blocked by " + info.transform.name);
                            continue;
                        }

                        float dist = Vector3.Distance(controller.transform.position, enemyController.transform.position);

                        if (dist < controller.OverallAttractRadius)
                        {
                            target = enemyController;
                            Debug.Log("Target " + enemyController.transform.name + " is in short range overall attract range.");
                        }
                        else
                        {
                            if (controller.TargetIsInsideVisionAngle(enemyController.transform.position))
                            {
                                target = enemyController;
                                Debug.Log("Target " + enemyController.transform.name + " inside the vision angle.");
                            }
                            else
                            {
                                Debug.Log("Target " + enemyController.transform.name + " is NOT inside the vision angle.");
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
                    //Debug.DrawLine(controller.Eye.position, target.transform.position, Color.yellow, 1f);
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

