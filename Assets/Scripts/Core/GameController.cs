using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Healthbar;
    
    private CinemachineVirtualCamera CineMachine;
    
    private PlayerMovement PlayerMovement;
    private HealthSystem health;
    private Timer Timer;
    
    private void Awake()
    {
        Instantiate(Player);
        Player = GameObject.FindWithTag("Player");

        PlayerMovement = Player.GetComponent<PlayerMovement>();

        HealthSetup();
        CameraSetup();
        TimerSetup();
        
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    private void HealthSetup()
    {
        health = Player.GetComponent<HealthSystem>();
        HealthBar hb =  Healthbar.GetComponent<HealthBar>();
        hb.playerHealth = Player.GetComponent<HealthSystem>();
        
        health.CurrentHealth = PlayerPrefs.GetFloat("HP",3);
    }

    private void CameraSetup()
    {
        CineMachine = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        CineMachine.Follow = Player.transform;
    }

    private void TimerSetup()
    {
        Timer = GameObject.Find("Timer").GetComponent<Timer>();
        Timer.HS = health;
    }

    public bool RespawnHelper()
    {
        if (health.CurrentHealth <= 1)
        {
            SceneManager.LoadScene("Menu");
            return true;
        }
        PlayerMovement.jumpPower = 15f;
        PlayerMovement.speed = 10f;
        
        PlayerPrefs.SetFloat("HP", health.CurrentHealth - 1);
        return false;
    }
}