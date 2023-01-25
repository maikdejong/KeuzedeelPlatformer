using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }

    [SerializeField] float iFramesDuration;
    [SerializeField] int numberOfFlashes;
    private SpriteRenderer _spriteRenderer;

    private bool _alive;
    private bool _isGameOver;

    public Sprite DeadSprite;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _alive = true;
        _isGameOver = false;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            StartCoroutine(InvulnerabilityAfterDamage());
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
            _alive = false;
            if (!_alive && !_isGameOver)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = DeadSprite;
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
                _isGameOver = true;
            }
        }
    }

    public void AddHealth(float healthValue)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthValue, 0, startingHealth);
    }

    private IEnumerator InvulnerabilityAfterDamage()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0.6f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 3));
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 3));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}