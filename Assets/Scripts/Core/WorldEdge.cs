using UnityEngine;

public class WorldEdge : MonoBehaviour
{
    private float damage = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }
}