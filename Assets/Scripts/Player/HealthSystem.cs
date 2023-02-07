using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float CurrentHealth { get; set; }

    [SerializeField] float iFramesDuration;
    [SerializeField] int numberOfFlashes;
    private SpriteRenderer _spriteRenderer;

    public bool _alive;
   
    public Sprite DeadSprite;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _alive = true;
    }

    public void TakeDamage(float damage)
    {
        //Logica voor het nemen van damage
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            StartCoroutine(InvulnerabilityAfterDamage());
        }
        else
        {
            _alive = false;
            if (!_alive )
            {
                Time.timeScale = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = DeadSprite;
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            }
        }
    }

    public void AddHealth(float healthValue)
    {
        //Healthcollectible
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthValue, 0, startingHealth);
    }

    
    private IEnumerator InvulnerabilityAfterDamage()
    {
        //Bestendig tegen damage na player+enemy collision + kleurverandering als visuele feedback
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