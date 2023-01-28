using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;

    private CinemachineVirtualCamera CineMachine;

    public GameObject Healthbar;
    
    
    private PlayerMovement PlayerMovement;
    private Rigidbody2D PlayerRigid;

    private Timer Timer;
    
    private float respawnDamage = 1;
    private HealthSystem health;

    private void Awake()
    {
        Instantiate(Player);
        
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        PlayerRigid = Player.GetComponent<Rigidbody2D>();
        health = Player.GetComponent<HealthSystem>();
        
        CameraSetup();
        
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerMovement.enabled = true;
        
        HealthBar hb =  Healthbar.GetComponent<HealthBar>();

        hb.playerHealth = Player.GetComponent<HealthSystem>();

        Timer = GameObject.Find("Timer").GetComponent<Timer>();
        Timer.PlayerMovement = PlayerMovement;
        Timer.PlayerRigid = PlayerRigid;
    }

    private void Update()
    {
        Debug.Log(Player.transform.position);
    }


    private void CameraSetup()
    {
        Transform PT = GameObject.FindWithTag("Player").transform;
        
        CineMachine = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        CineMachine.Follow = PT;
        CineMachine.LookAt = PT;
        
        
    }
    
    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
        Player.transform.position = new Vector3(23, 5, 0);
        PlayerRigid.constraints = RigidbodyConstraints2D.None;
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
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
