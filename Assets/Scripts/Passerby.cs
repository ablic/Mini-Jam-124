using ReactiveProperties;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passerby : Character, IInteractable
{
    [SerializeField] private FloatProperty minHorizontalVelocity;
    [SerializeField] private FloatProperty maxHorizontalVelocity;
    [SerializeField] private Item favoriteItem;
    [SerializeField] private GameObject heartParticles;
    [SerializeField] private AudioClip[] itemReceivingSounds;

    private float velocity;

    public event System.Action Interacted;

    public Transform Transform => transform;

    public Vector2 Direction { get; set; }

    private void Start()
    {
        velocity = Random.Range(
            minHorizontalVelocity.Value,
            maxHorizontalVelocity.Value);
    }

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
            player.Warmth.Value += 0.2f;
            player.Drop();
            player.PlaySound(itemReceivingSounds.GetRandomElement());
            heartParticles.SetActive(true);
            Interacted?.Invoke();

            return true;
        }

        return false;
    }
}
