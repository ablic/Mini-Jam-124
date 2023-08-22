using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Store : MonoBehaviour, IInteractable
{
    [SerializeField] private Item itemForSale;
    [SerializeField] private AudioClip[] buySounds;
    [SerializeField] private Image itemView;

    public event System.Action Interacted;

    public Transform Transform => transform;

    private void Awake()
    {
        itemView.sprite = itemForSale.View;
    }

    public bool TryInteract(Player player)
    {
        if (player.CarriedItem != null)
            return false;

        player.PickUp(itemForSale);
        player.PlaySound(buySounds.GetRandomElement());

        Interacted?.Invoke();

        return true;
    }
}