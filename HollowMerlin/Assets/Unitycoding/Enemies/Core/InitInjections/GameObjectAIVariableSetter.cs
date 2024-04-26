using System.Collections;
using System.Collections.Generic;
using ICode;
using UnityEngine;

public class GameObjectAIVariableSetter : MonoBehaviour, IAIInit
{

    [SerializeField]
    private string variableName;
    [SerializeField]
    private GameObject[] gameObjects;

    public void Init(ICodeBehaviour behaviour) {
        behaviour.stateMachine.SetVariable(variableName, gameObjects as object[]);
    }
}
