using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Respawn()
    {
        //checkt in de respawnHelper functie in GameController script, of de player nog 'leeft'
        //als de player leeft: !respawnHelper
        bool respawnHelper = GameObject.Find("GameController").GetComponent<GameController>().RespawnHelper();
        if (!respawnHelper)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}