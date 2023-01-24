using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }

    [SerializeField] float iFramesDuration;
    [SerializeField] int numberOfFlashes;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
            _spriteRenderer.color = new Color(1, 0, 0);
        }
    }
    
    public void AddHealth(float healthValue)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthValue, 0, startingHealth);
    }
    
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9,10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes) / 4);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes) / 4);
        }
        Physics2D.IgnoreLayerCollision(9,10, false);
    }
}
