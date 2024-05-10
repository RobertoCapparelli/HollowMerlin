using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;


[Category("EnemyMovement")]
[Tooltip("Apply an vertical impulse to the enemy")]

public class SetVerticalVelocity : StateAction
{
    [Tooltip("Owner GameObject")]
    public FsmGameObject GameObject;
    [Tooltip("Speed (Frequency)")]
    public FsmFloat Speed;
    [Tooltip("Amplitude")]
    public FsmFloat Amplitude;

    private EnemyComponent enemyComponent;
    

    public override void OnEnter()
    {
        enemyComponent = GameObject.Value.GetComponent<EnemyComponent>();

        enemyComponent.SetVerticalMovement(Speed, Amplitude);

        Finish();

    }

}
