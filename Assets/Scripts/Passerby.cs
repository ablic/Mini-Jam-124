using ReactiveProperties;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passerby : Character, IInteractable
{
    [SerializeField] private FloatProperty minHorizontalVelocity;
    [SerializeField] private FloatProperty maxHorizontalVelocity;
    [SerializeField] private FloatProperty warmthIncrease;
    [SerializeField] private Item favoriteItem;
    [SerializeField] private GameObject heartParticles;
    [SerializeField] private AudioClip[] itemReceivingSounds;

    public event System.Action Interacted;

    public Transform Transform => transform;

    public Vector2 Direction { get; set; }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 25f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move(Direction);
    }

    public bool TryInteract(Player player)
    {
        if (player.CarriedItem != null && CarriedItem == null && 
            (favoriteItem == player.CarriedItem || favoriteItem == null))
        {
            PickUp(player.CarriedItem);
            player.Warmth.Value += warmthIncrease.Value;
            player.Drop();
            player.PlaySound(itemReceivingSounds.GetRandomElement());
            heartParticles.SetActive(true);
            Interacted?.Invoke();

            return true;
        }

        return false;
    }
}
