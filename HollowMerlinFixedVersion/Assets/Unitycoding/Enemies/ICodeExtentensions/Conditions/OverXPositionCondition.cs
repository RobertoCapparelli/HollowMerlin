using ICode.Conditions;
using UnityEngine;
using ICode;
using Tooltip = ICode.TooltipAttribute;

[Category(Category.Vector3)]
[Tooltip("Over another gameobject x position.")]
[System.Serializable]
public class OverXPositionCondition : Condition
{
    [Tooltip("Moving gameobject")]
    public FsmGameObject owner;
    [Tooltip("Target tocheck")]
    public FsmGameObject target;


    public override bool Validate() {
        Vector3 localTargetPosition = owner.Value.transform.
            InverseTransformPoint(target.Value.transform.position);
        return localTargetPosition.x < 0.1;
    }

}
