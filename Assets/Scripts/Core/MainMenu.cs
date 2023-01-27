using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject Player;
    private PlayerMovement PlayerMovement;
    private Rigidbody2D PlayerRigid;

    private Timer Timer;
    
    private float respawnDamage = 1;
    private HealthSystem health;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PlayerRigid = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Timer = GameObject.Find("Timer").GetComponent<Timer>();
        health = GameObject.Find("Player").GetComponent<HealthSystem>();
    }

    
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
        Player.GetComponent<Transform>().position = new Vector3(23, 3, 0);
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerMovement.enabled = true;
        PlayerMovement.jumpPower = 15f;
        PlayerMovement.speed = 10f;
        SceneManager.UnloadSceneAsync("TimeUp");
        health.TakeDamage(respawnDamage);
        Timer.TimeLeft = 30f;
        Timer.TimerOn = true;
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
