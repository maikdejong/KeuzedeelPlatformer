using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.SetFloat("HP", 3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}