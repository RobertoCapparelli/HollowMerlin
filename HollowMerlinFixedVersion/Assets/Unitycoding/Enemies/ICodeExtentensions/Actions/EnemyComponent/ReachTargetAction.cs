using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;

[Category("EnemyMovement")]
[Tooltip("Set face direction of the component")]
public class ReachTargetAction : StateAction
{

    [Tooltip("Enemy GameObject")]
    public FsmGameObject gameObject;
    [Tooltip("Face direction value")]
    public FsmGameObject target;

    private EnemyComponent enemyComponent;

    public override void OnEnter() {
        enemyComponent = gameObject.Value.GetComponent<EnemyComponent>();
        if (enemyComponent == null) {
            Finish();
            return;
        }
        InternalSetDirection();
    }

    public override void OnUpdate() {
        InternalSetDirection();
    }

    private void InternalSetDirection() {
        Vector3 inputDirection;
        if (target.Value.transform.position.x > gameObject.Value.transform.position.x) {
            inputDirection = Vector2.right;
        } else {
            inputDirection = Vector2.left;
        }
        enemyComponent.SetInputDirection(inputDirection);
    }


}
