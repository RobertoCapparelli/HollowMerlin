namespace ICode.Actions.Variable {
    [Category(Category.Variable)]
    [Tooltip("Module the int value of a variable.")]
    [System.Serializable]
    public class ModuleIntValue : StateAction {
        [Shared]
        [Tooltip("The variable to use.")]
        public FsmInt variable;
        [Tooltip("The maxValue to set.")]
        public FsmInt maxValue;

        public override void OnEnter() {
            variable.Value = variable.Value % maxValue;
            Finish();
        }
    }
}
