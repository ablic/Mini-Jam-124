using System.Collections;
using UnityEngine;

public class InteractionVisualizer : MonoBehaviour
{
    [SerializeField] private Vector2 targetScale = new Vector2(1.2f, 0.8f);
    [SerializeField, Min(0f)] private float restoreSpeed = 5f;

    private void Awake()
    {
        if (TryGetComponent(out IInteractable interactable))
            interactable.Interacted += Visualize;
    }

    private void Visualize()
    {
        transform.localScale = targetScale;
        StartCoroutine(RestoreScale());
    }

    private IEnumerator RestoreScale()
    {
        while (Mathf.Abs(1f - transform.localScale.x) > 0.02f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, restoreSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
