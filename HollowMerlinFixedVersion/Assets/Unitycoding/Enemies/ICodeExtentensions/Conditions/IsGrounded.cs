using ICode.Conditions;
using UnityEngine;
using ICode;
using Tooltip = ICode.TooltipAttribute;

[Category(Category.EnemyComponent)]
[Tooltip("If enemy is graunded.")]
[System.Serializable]
public class IsGrounded : Condition
{
    [Tooltip("Owner gameobject")]
    public FsmGameObject owner;

    private EnemyComponent enemyComponent;


    public override bool Validate() {
        enemyComponent = owner.Value.GetComponent<EnemyComponent>();

        return enemyComponent.IsGrounded;

    }

}
