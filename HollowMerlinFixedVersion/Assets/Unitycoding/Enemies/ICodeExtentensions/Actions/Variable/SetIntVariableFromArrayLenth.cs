namespace ICode.Actions.Variable {
    [Category(Category.Variable)]
    [Tooltip("Clamp the int value of a variable.")]
    [System.Serializable]
    public class SetIntVariableFromArrayLenth : StateAction {
        [Shared]
        [Tooltip("The variable to use.")]
        public FsmInt variable;
        [Tooltip("The minValue to set.")]
        public FsmArray array;

        public override void OnEnter() {
            variable.Value = array.Value.Length;
            Finish();
        }
    }
}
