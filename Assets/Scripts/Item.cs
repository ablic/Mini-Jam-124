using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite view;

    public Sprite View => view;
}
