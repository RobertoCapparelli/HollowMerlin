using UnityEngine;

namespace ICode.Actions.Variable
{
    [Category(Category.Variable)]
    [Tooltip("Sets the GameObject value of a variable.")]
    [System.Serializable]
    public class SetGameObjectFromArray : StateAction
    {
        [Shared]
        [Tooltip("The variable to use.")]
        public FsmGameObject variable;
        [Tooltip("The array.")]
        public FsmArray array;
        [Tooltip("The index of the element to set")]
        public FsmInt index;

        public override void OnEnter()
        {
            variable.Value = (GameObject)array.Value[index.Value];
        }
    }
}

