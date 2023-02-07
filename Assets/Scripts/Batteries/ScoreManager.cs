using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI text;
    int _score;
    private GameObject _portal;
    
    void Start()
    {
        _portal = GameObject.Find("Portal");
        _portal.SetActive(false);
        
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void SpawnPortal()
    {
        //Spawn portal als score 3 is en dus 3 batterijen zijn verzameld
        if (_score == 3)
        {
            _portal.SetActive(true);
        }
    }

    public void ChangeScore(int batteryValue)
    {
        _score += batteryValue;
        text.text = _score.ToString() + " / 3";
        SpawnPortal();
    }
}
