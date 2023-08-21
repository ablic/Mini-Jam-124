using System;

public interface IInteractable
{
    event Action Interacted;
    bool TryInteract(Player player);
}
