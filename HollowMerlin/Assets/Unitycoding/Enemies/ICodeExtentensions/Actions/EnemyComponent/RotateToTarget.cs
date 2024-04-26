using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;

[Category("EnemyMovement")]
[Tooltip("Set face direction of the component")]
public class RotateToTarget : StateAction
{

    [Tooltip("Enemy GameObject")]
    public FsmGameObject gameObject;
    [Tooltip("Face direction value")]
    public FsmGameObject target;
    [Tooltip("EveryFrame")]
    public FsmBool everyFrame;

    public override void OnEnter() {
        InternalRotate();
        if (!everyFrame.Value) {
            Finish();
            return;
        }
    }

    public override void OnUpdate() {
        InternalRotate();
    }

    private void InternalRotate() {
        Vector3 eulerRotation;
        if (target.Value.transform.position.x > gameObject.Value.transform.position.x) {
            eulerRotation = Vector3.zero;
        } else {
            eulerRotation = new Vector3(0, 180, 0);
        }
        gameObject.Value.transform.eulerAngles = eulerRotation;
    }
}
