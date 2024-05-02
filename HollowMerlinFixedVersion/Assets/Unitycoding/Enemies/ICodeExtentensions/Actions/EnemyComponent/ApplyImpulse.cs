using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;


[Category("EnemyMovement")]
[Tooltip("Apply an impulse to the enemy")]

public class ApplyImpulse : StateAction
{
    [Tooltip("Owner GameObject")]
    public FsmGameObject GameObject;
    [Tooltip("Force impulse")]
    public FsmFloat Force;


    private EnemyComponent enemyComponent;

    public override void OnEnter()
    {
        enemyComponent = GameObject.Value.GetComponent<EnemyComponent>();
        if (enemyComponent == null)
        {
            Finish();
            return;
        }
        ApplyImpulseToEnemy();
        Finish();
    }

    public override void OnUpdate()
    {
        ApplyImpulseToEnemy();
    }

    private void ApplyImpulseToEnemy()
    {
        enemyComponent.SetJumpForce(Force.Value);
        enemyComponent.Jump();
    }

}
