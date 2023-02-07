using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;

    public HealthSystem HS;

    private void Start()
    {
        TimerOn = true;
    }

    private void FixedUpdate()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0f)
            {
                TimeLeft -= Time.deltaTime;
                ChangeTimer(TimeLeft);
                if (HS._alive == false)
                {
                    TimerOn = false;
                }
            }
            else
            {
                TimeLeft = 0f;
                TimerOn = false;
            }
        }
    }

    private void ChangeTimer(float currentTime)
    {
        currentTime += 1f;

        //zorgt ervoor dat de timer alleen hele getallen gebruikt
        float seconds = Mathf.FloorToInt(currentTime % 60f);


        if (TimeLeft > 0f)
        {
            //zorgt ervoor dat de timer alleen hele getallen gebruikt
            TimerText.text = string.Format("{0:00}", seconds);
        }
        else
        {
            TimerText.text = "Time's up!";
            if (HS._alive)
            {
                SceneManager.LoadScene("TimeUp", LoadSceneMode.Additive);
                Time.timeScale = 0;
            }
        }
    }
}