using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Respawn()
    {
        bool respawnHelper =  GameObject.Find("GameController").GetComponent<GameController>().RespawnHelper();
        if (!respawnHelper)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
