using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    int score;
    private GameObject _Portal;
    
    void Start()
    {
        _Portal = GameObject.Find("Portal");
        _Portal.SetActive(false);
        
        if (instance == null)
        {
            instance = this;
        }
    }

    void spawnPortal()
    {
        if (score == 3)
        {
            _Portal.SetActive(true);
        }
    }

    public void ChangeScore(int batteryValue)
    {
        score += batteryValue;
        text.text = score.ToString() + " / 3";
        spawnPortal();
    }
}
