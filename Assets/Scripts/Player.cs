using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : Character
{
    [SerializeField] private Warmth warmth;
    [SerializeField] private CircleCollider2D interactionRangeCollider;

    private AudioSource audioSource;
    private HashSet<IInteractable> targets = new HashSet<IInteractable>();

    public Warmth Warmth => warmth;

    private bool AnyInteractionKeyPressed
    {
        get
        {
            foreach (var key in GameManager.Config.Player.InteractionKeys)
                if (Input.GetKeyDown(key))
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
        UpdateInteractionRange();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (AnyInteractionKeyPressed)
            Interact();

#if UNITY_EDITOR
        UpdateInteractionRange();
#endif
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")).normalized;

        Move(new Vector2(
            direction.x * GameManager.Config.Player.HorizontalVelocity,
            direction.y * GameManager.Config.Player.VerticalVelocity));
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

    private void UpdateInteractionRange()
    {
        interactionRangeCollider.radius = GameManager.Config.Player.InteractionRange;
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
