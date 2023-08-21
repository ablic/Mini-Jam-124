using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : Character
{
    [SerializeField] private KeyCode interactButton1 = KeyCode.Mouse0;
    [SerializeField] private KeyCode interactButton2 = KeyCode.Space;
    [SerializeField] private Warmth warmth;

    private AudioSource audioSource;
    private Dictionary<Transform, IInteractable> targets = new Dictionary<Transform, IInteractable>();

    public Warmth Warmth => warmth;
    private IInteractable NearestTarget
    {
        get
        {
            IInteractable nearestTarget = null;
            float minSqrDistance = Mathf.Infinity;

            foreach (var item in targets)
            {
                float sqrDistance = (transform.position - item.Key.position).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    nearestTarget = item.Value;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(interactButton1) || Input.GetKeyDown(interactButton2))
            Interact();
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Move(new Vector2(inputX, inputY));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable target) && !targets.ContainsKey(collision.transform))
            targets.Add(collision.transform, target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable target) && targets.ContainsKey(collision.transform))
            targets.Remove(collision.transform);
    }

    public void Interact()
    {
        if (targets.Count == 0)
            return;

        if (!NearestTarget.TryInteract(this))
        {
            foreach (var target in targets.Values)
            {
                if (target.TryInteract(this))
                    return;
            }
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
