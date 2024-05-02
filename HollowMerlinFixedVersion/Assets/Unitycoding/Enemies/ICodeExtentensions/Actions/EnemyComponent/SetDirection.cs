using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;

[Category("EnemyMovement")]
[Tooltip("Set face direction with the turn animation")]
public class SetDirection : StateAction
{
    [Tooltip("Enemy GameObject")]
    public FsmGameObject gameObject;
    [Tooltip("Face direction value")]
    public FsmBool faceDirection;
    [Tooltip("Set the speed at every frame?")]
    public FsmBool everyFrame;

    private EnemyComponent enemyComponent;

    public override void OnEnter()
    {
        enemyComponent = gameObject.Value.GetComponent<EnemyComponent>();
        if (enemyComponent == null)
        {
            Finish();
            return;
        }
        InternalFaceDireciont();
        if (!everyFrame.Value) Finish();
    }

    public override void OnUpdate()
    {
        InternalFaceDireciont();
    }

    private void InternalFaceDireciont()
    {
        enemyComponent.SetFaceDirection(faceDirection.Value);
    }
}
