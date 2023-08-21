using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField, Min(0f)] protected float walkSpeed = 5f;
    [SerializeField] private SpriteRenderer cargo;

    private Rigidbody2D rbody;
    private Animator animator;

    public Item CarriedItem { get; set; }

    protected virtual void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) != Mathf.Abs(direction.y))
            cargo.transform.localPosition = new Vector3(direction.x * 0.75f, direction.y * 0.5f + 0.5f, 0.1f);

        direction = new Vector2(direction.x, direction.y * 0.5f).normalized;

        rbody.velocity = direction * walkSpeed;

        animator.SetFloat("Abs Horizontal Move", Mathf.Abs(direction.x));
        animator.SetFloat("Abs Vertical Move", Mathf.Abs(direction.y));
        animator.SetFloat("Horizontal Move", direction.x);
        animator.SetFloat("Vertical Move", direction.y);            
    }

    public void PickUp(Item item)
    {
        if (item == null)
            return;

        CarriedItem = item;
        cargo.sprite = item.View;
    }

    public void Drop()
    {
        if (CarriedItem == null)
            return;

        CarriedItem = null;
        cargo.sprite = null;
    }
}
