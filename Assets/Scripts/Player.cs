using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : Character
{
    [SerializeField] private GameConfig config;
    [SerializeField] private Warmth warmth;
    [SerializeField] private CircleCollider2D interactionRangeCollider;

    private AudioSource audioSource;
    private HashSet<IInteractable> targets = new HashSet<IInteractable>();

    public Warmth Warmth => warmth;

    private bool AnyInteractionKeyPressed
    {
        get
        {
            foreach (var keyCode in config.PlayerInteractionKeys)
                if (Input.GetKeyDown(keyCode))
                    return true;

            return false;
        }
    }

    private IInteractable NearestTarget
    {
        get
        {
            if (targets.Count == 0)
                return null;

            IInteractable nearestTarget = null;
            float minSqrDistance = Mathf.Infinity;            

            foreach (var item in targets)
            {
                float sqrDistance = (transform.position - item.Transform.position).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    nearestTarget = item;
                    minSqrDistance = sqrDistance;
                }
            }

            return nearestTarget;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        config.PlayerInteractionRange.Changed += UpdateInteractionRadius;
    }

    private void OnDisable()
    {
        config.PlayerInteractionRange.Changed -= UpdateInteractionRadius;
    }

    private void Start()
    {
        interactionRangeCollider.radius = config.PlayerInteractionRange.Value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (AnyInteractionKeyPressed)
            Interact();
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")).normalized;

        Move(new Vector2(
            direction.x * config.PlayerHorizontalVelocity,
            direction.y * config.PlayerVerticalVelocity));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
            targets.Add(interactable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
            targets.Remove(interactable);
    }

    public void Interact()
    {
        IInteractable interactable = NearestTarget;

        if (interactable == null)
            return;

        if (!interactable.TryInteract(this))
        {
            targets.Remove(interactable);
            Interact();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void UpdateInteractionRadius(float value)
    {
        interactionRangeCollider.radius = value;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRangeCollider.radius);

        var target = NearestTarget;

        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.Transform.position, 0.2f);
        }
    }
#endif
}
