using UnityEngine;

public class FishSwim3D : MonoBehaviour
{
    public float speed = 3f;
    private int direction = 1;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime, Space.World);

        // If out of bounds, return to pool
        if (Mathf.Abs(transform.position.x) > 15f) // adjust boundary to screen size
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
        }
    }

    public void Init(int dir, float yPos)
    {
        direction = dir;

        // Set spawn position based on direction
        transform.position = new Vector3(dir > 0 ? -15f : 15f, yPos, 0f);

        // Rotate fish to face movement direction
        if (dir > 0)
        {
            // Moving right ? face right
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            // Moving left ? face left
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }
}
