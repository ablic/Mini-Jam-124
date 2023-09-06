using UnityEngine;

namespace ReactiveProperties
{
    [CreateAssetMenu(fileName = "PropertySet")]
    public class PropertySet : ScriptableObject
    {
        [field: SerializeField]
        public ReactivePropertyBase[] Properties { get; private set; }
    }
}