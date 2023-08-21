using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Store : MonoBehaviour, IInteractable
{
    [SerializeField] private Item[] itemsForSale;
    [SerializeField] private AudioClip[] buySounds;

    public event System.Action Interacted;

    public bool TryInteract(Player player)
    {
        if (player.CarriedItem != null)
        {
            Debug.Log($"Player already has carried item ({player.CarriedItem.name})");
            return false;
        }

        player.PickUp(itemsForSale[Random.Range(0, itemsForSale.Length)]);
        player.PlaySound(buySounds.GetRandomElement());

        Interacted?.Invoke();

        return true;
    }
}