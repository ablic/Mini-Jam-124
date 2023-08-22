using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Trash : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip[] dropSounds;

    public event Action Interacted;

    public Transform Transform => transform;

    public bool TryInteract(Player player)
    {
        if (player.CarriedItem != null)
        {
            player.Drop();
            player.PlaySound(dropSounds.GetRandomElement());
            Interacted?.Invoke();
            return true;
        }

        return false;
    }
}
