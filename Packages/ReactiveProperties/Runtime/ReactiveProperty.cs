using System;
using UnityEngine;

namespace ReactiveProperties
{
    public class ReactiveProperty<T> : ReactivePropertyBase
    {
        [SerializeField]
        protected T value;

        public virtual T Value
        {
            get { return value; }
            set 
            {
                this.value = value; 
                Changed?.Invoke(value); 
            }
        }

        public event Action<T> Changed;

        protected virtual void OnValidate()
        {
            Changed?.Invoke(value);
        }
    }
}