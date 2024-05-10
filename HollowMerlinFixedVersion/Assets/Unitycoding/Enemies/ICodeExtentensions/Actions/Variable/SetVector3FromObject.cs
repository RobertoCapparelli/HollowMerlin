using UnityEngine;
using System.Collections;
using ICode;
using ICode.Actions;
using TooltipAttribute = ICode.TooltipAttribute;

[Category(Category.Variable)]
[Tooltip("Sets the Vector3 value of a variable.")]
[System.Serializable]
public class SetVector3FromObject : StateAction
{
    [Shared]
    [Tooltip("The variable to use.")]
    public FsmVector3 variable;
    [Tooltip("The GameObject to take the position.")]
    public FsmGameObject value;
    [Tooltip("Execute the action every frame.")]
    public bool everyFrame;

    public override void OnEnter()
    {
        variable.Value = value.Value.transform.position;
        if (!everyFrame)
        {
            Finish();
        }
    }

    public override void OnUpdate()
    {
        variable.Value = value.Value.transform.position;
    }
}
