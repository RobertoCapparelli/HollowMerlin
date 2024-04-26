using UnityEngine;

namespace ICode.Conditions {
    [Category(Category.Vector3)]
    [Tooltip("Distance between two Vector3 points on x axis.")]
    public class DistanceOnXAxisCondition : Condition {
        [NotRequired]
        [Tooltip("Vector3 value.")]
        public FsmVector3 a;
        [SharedPersistent]
        [NotRequired]
        public FsmGameObject first;
        [NotRequired]
        [Tooltip("Vector3 value.")]
        public FsmVector3 b;
        [Shared]
        [NotRequired]
        public FsmGameObject second;
        [Tooltip("Is the distance greater or less?")]
        public FloatComparer comparer;
        [Tooltip("Value to test with.")]
        public FsmFloat value;


        public override bool Validate() {
            float distanceOnXAxis = Mathf.Abs(FsmUtility.GetPosition(first, a).x - FsmUtility.GetPosition(second, b).x);
            return FsmUtility.CompareFloat(distanceOnXAxis, value.Value, comparer);
        }
    }
}
