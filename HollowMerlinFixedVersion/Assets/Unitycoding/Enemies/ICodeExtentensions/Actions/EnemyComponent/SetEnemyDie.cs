using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;

[Category("EnemyMovement")]
[Tooltip("Set Enemy death")]

public class SetEnemyDie : StateAction
{
    [Tooltip("Enemy GameObject")]
    public FsmGameObject gameObject;

    private EnemyComponent enemyComponent;

    public override void OnEnter()
    {
        enemyComponent = gameObject.Value.GetComponent<EnemyComponent>();
        if (enemyComponent == null)
        {
            Finish();
            return;
        }

        enemyComponent.Die();
        Finish();

    }


}

