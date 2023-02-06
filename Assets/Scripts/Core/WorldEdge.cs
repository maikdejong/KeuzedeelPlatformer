using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("TimeUp", LoadSceneMode.Additive);
            SceneManager.LoadScene("OutOfBounds", LoadSceneMode.Additive);
            Time.timeScale = 0;
        }
    }
}