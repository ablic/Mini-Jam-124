using System;
using UnityEngine;

public interface IInteractable
{
    event Action Interacted;
    Transform Transform { get; }
    bool TryInteract(Player player);
}
