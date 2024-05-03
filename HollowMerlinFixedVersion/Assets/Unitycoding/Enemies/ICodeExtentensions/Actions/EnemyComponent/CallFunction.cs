using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;
using System.Collections.Generic;
using ICode.FSMEditor;
using System.Reflection;

[Category("EnemyMovement")]
[Tooltip("Call Function in the EnemyComponent")]

/*
 * Questa azione in realtà è solo a scopo didattico, volevo provare a creare un azione che potesse chiamare qualsiasi metodo dell'enemy component,
 * però seguendo la logica che una volta che hai creato un azione rendendola generica, poi puoi riutilizzarla in più occasioni, ha più senso creare
 * una azione per ogni metodo dell'enemy component e successivamente richiamare direttamente quella che ti serve.
 * 
 * Ho provato a "modificare" il codice di ICode per far si che le extendedVariable potessero essere create direttamente nella stateMachine ma 
 * al momento è solo possibile passare la variabile ExtendedVariable dalle variabili generali della FSM e creare la variabile dall'enemy component.
 * 
 * "modificare" = ho usato chatGPT perchè se no passavo 1 settimana solo a capire il funzionamento della FsmVariableDrawer.

*/
public class CallFunction : StateAction
{
    [Tooltip("Enemy GameObject")]
    public FsmGameObject GameObject;
    [Tooltip("Method name to call")]
    public FsmString MethodName;
    [Tooltip("arguments for the method")]
    public FsmExtendedVariable Argument1;
    [Tooltip("arguments for the method")]
    public FsmExtendedVariable Argument2;

    private EnemyComponent enemyComponent;

    public override void OnEnter()
    {
        enemyComponent = GameObject.Value.GetComponent<EnemyComponent>();
        if (enemyComponent == null)
        {
            Finish();
            return;
        }

        MethodInfo methodInfo = enemyComponent.GetType().GetMethod(MethodName.Value);
        if (methodInfo == null) { Finish(); return; }

        if(methodInfo.GetParameters().Length == 0 )
        {
            methodInfo.Invoke(enemyComponent, null);
            Finish();
            return;
        }

        object[] arguments = GetMethodArguments();

        methodInfo.Invoke(enemyComponent, arguments);

        Finish();
    }
    private object[] GetMethodArguments()
    {
        List<object> arguments = new List<object>();

        if (Argument1 != null)
        {
            arguments.Add(Argument1.GetValue());
        }

        if (Argument2 != null)
        {
            arguments.Add(Argument2.GetValue());
        }

        return arguments.ToArray();
    }


}
