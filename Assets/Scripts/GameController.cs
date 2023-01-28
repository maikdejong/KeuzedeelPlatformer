using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Healthbar;
    
    private CinemachineVirtualCamera CineMachine;
    
    private PlayerMovement PlayerMovement;
    private Rigidbody2D PlayerRigid;
    private HealthSystem health;
    private Timer Timer;
    
    private float respawnDamage = 1;

    private void Awake()
    {
        Instantiate(Player);
        Player = GameObject.FindWithTag("Player");
        
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        PlayerRigid = Player.GetComponent<Rigidbody2D>();
        
        health = Player.GetComponent<HealthSystem>();
        HealthBar hb =  Healthbar.GetComponent<HealthBar>();
        hb.playerHealth = Player.GetComponent<HealthSystem>();

        CameraSetup();
        
        Timer = GameObject.Find("Timer").GetComponent<Timer>();
        Timer.PlayerMovement = PlayerMovement;
        Timer.PlayerRigid = PlayerRigid;
        Timer.HS = health;
    }

    private void CameraSetup()
    {
        CineMachine = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        CineMachine.Follow = Player.transform;
    }
    
    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
        Player.transform.position = new Vector3(23, 5, 0);
        PlayerMovement.enabled = true;
        PlayerMovement.jumpPower = 15f;
        PlayerMovement.speed = 10f;
        health.TakeDamage(respawnDamage);
    }
    
    public void RestartGame()
    {
        DestroyAllDontDestroyOnLoadObjects();
        SceneManager.LoadScene(0);
    }
    
    private void DestroyAllDontDestroyOnLoadObjects() {

        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach(var root in go.scene.GetRootGameObjects())
            Destroy(root);
    }
    
}
