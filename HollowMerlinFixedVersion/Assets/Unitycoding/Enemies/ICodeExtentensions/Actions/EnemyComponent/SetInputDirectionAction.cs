using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;

[Category("EnemyMovement")]
[Tooltip("Set the input direction to the player for the movement")]
public class SetInputDirectionToPlayerAction : StateAction
{

    [Tooltip("Owner GameObject")]
    public FsmGameObject Owner;
    [Tooltip("Direction to set")]
    public FsmGameObject Target;
    [Tooltip("Set the speed at every frame?")]
    public FsmBool everyFrame;

    private EnemyComponent owner;

    public override void OnEnter() {
        owner = Owner.Value.GetComponent<EnemyComponent>();
        if (owner == null) {
            Finish();
            return;
        }
        InternalSetInpiutDirection();
        if (!everyFrame.Value) Finish();
    }

    public override void OnUpdate() {
        InternalSetInpiutDirection();
    }

    private void InternalSetInpiutDirection() {
        owner.SetInputDirection(Target.Value.transform.position);
    }

}
