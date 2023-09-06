using UnityEngine;

namespace ReactiveProperties
{
    [CreateAssetMenu(menuName = "ReactiveProperty/Float")]
    public class FloatProperty : ReactiveProperty<float>
    {
        [SerializeField] private float min = 0f;
        [SerializeField] private float max = 100f;

        public override float Value
        {
            get => this.value;
            set
            {
                this.value = Mathf.Clamp(value, min, max);
            }
        }
    }
}