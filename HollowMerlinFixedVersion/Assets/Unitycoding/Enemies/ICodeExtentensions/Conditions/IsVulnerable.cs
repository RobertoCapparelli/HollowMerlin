using ICode.Conditions;
using UnityEngine;
using ICode;
using Tooltip = ICode.TooltipAttribute;

[Category(Category.EnemyComponent)]
[Tooltip("If enemy is vulnerable.")]
[System.Serializable]
public class IsVulnerable : Condition
{
    [Tooltip("Owner gameobject")]
    public FsmGameObject owner;

    private EnemyComponent enemyComponent;


    public override bool Validate() {
        enemyComponent = owner.Value.GetComponent<EnemyComponent>();

        return enemyComponent.IsVulnerable;

    }

}
