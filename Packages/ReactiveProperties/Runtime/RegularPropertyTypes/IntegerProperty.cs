using UnityEngine;

namespace ReactiveProperties
{
    [CreateAssetMenu(menuName = "ReactiveProperty/Integer")]
    public class IntegerProperty : ReactiveProperty<int>
    {
        [SerializeField] private int min = 0;
        [SerializeField] private int max = 100;

        public override int Value 
        {
            get => this.value; 
            set
            {
                this.value = Mathf.Clamp(value, min, max);
            }
        }
    }
}