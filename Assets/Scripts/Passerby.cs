using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passerby : Character, IInteractable
{
    [SerializeField] private GameConfig config;
    [SerializeField] private Item favoriteItem;
    [SerializeField] private GameObject heartParticles;
    [SerializeField] private AudioClip[] itemReceivingSounds;

    private bool isHappy = false;

    public event System.Action Interacted;

    public Transform Transform => transform;

    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 25f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move(Direction * Speed);
    }

    public bool TryInteract(Player player)
    {
        if (isHappy || favoriteItem != null && favoriteItem != player.CarriedItem)
            return false;

        PickUp(player.CarriedItem);
        player.Warmth.Value += config.PasserbyWarmthIncrease;
        player.Drop();
        player.PlaySound(itemReceivingSounds.GetRandomElement());
        heartParticles.SetActive(true);
        Interacted?.Invoke();
        isHappy = true;

        return true;
    }
}
