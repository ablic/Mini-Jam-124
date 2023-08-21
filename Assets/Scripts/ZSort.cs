using UnityEngine;

public class ZSort : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
