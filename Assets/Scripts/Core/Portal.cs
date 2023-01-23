using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1, LoadSceneMode.Single);
            collision.gameObject.GetComponent<Transform>().position = new Vector3(-6, 3, 0);
        }
    }
}