using UnityEngine;
using ICode;
using ICode.Actions;
using Tooltip = ICode.TooltipAttribute;
using System.Collections.Generic;
using ICode.FSMEditor;

[Category("EnemyMovement")]
[Tooltip("Call Function in the EnemyComponent")]


public class CallFunction : StateAction
{
    [Tooltip("Enemy GameObject")]
    public FsmGameObject gameObject;
    [Tooltip("Method name to call")]
    public FsmString methodName;
    [Tooltip("Dictionary of arguments for the method")]
    public FsmExtendedVariable test;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
