using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;

    private void Start()
    {
        TimerOn = true;
    }

    private void FixedUpdate()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                ChangeTimer(TimeLeft);
            }
            else
            {
                Debug.Log("TIJDOP");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    private void ChangeTimer(float TimeLeft)
    {
        TimerText.text = TimeLeft.ToString();
    }
}
