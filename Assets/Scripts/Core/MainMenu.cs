using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float respawnDamage;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        DestroyAllDontDestroyOnLoadObjects();
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(-6, 3, 0);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
    private void DestroyAllDontDestroyOnLoadObjects() {

        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach(var root in go.scene.GetRootGameObjects())
            Destroy(root);
    }
}
