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
    
    //Dit script wordt aan het begin van elke nieuwe LevelScene gecalled
    
    private void Awake()
    {
        //Cloned de Player prefab
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
        //assigned alle health logica aan de geclonede speler
        health = Player.GetComponent<HealthSystem>();
        HealthBar hb =  Healthbar.GetComponent<HealthBar>();
        hb.playerHealth = Player.GetComponent<HealthSystem>();
        
        health.CurrentHealth = PlayerPrefs.GetFloat("HP",3);
    }

    private void CameraSetup()
    {
        //zorgt ervoor dat CineMachine de speler volgt
        CineMachine = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        CineMachine.Follow = Player.transform;
    }

    private void TimerSetup()
    {
        //zorgt ervoor dat Timer script gebruikt wordt
        Timer = GameObject.Find("Timer").GetComponent<Timer>();
        Timer.HS = health;
    }

    public bool RespawnHelper()
    {
        //Checkt of de speler naar nog meer dan 1 leven heeft wanneer er op respawn-button wordt geklikt
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