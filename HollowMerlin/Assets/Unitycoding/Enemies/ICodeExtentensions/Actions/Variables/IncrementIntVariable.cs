namespace ICode.Actions.Variable
{
    [Category(Category.Variable)]
    [Tooltip("Increment the int value of a variable.")]
    [System.Serializable]
    public class IncrementIntVariable : StateAction
    {
        [Shared]
        [Tooltip("The variable to use.")]
        public FsmInt variable;
        [Tooltip("The value to set.")]
        public FsmInt value;

        public override void OnEnter()
        {
            variable.Value += value.Value;
            Finish();
        }

    }
}

