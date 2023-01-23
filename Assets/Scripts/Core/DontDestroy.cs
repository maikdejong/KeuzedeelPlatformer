using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] healthCanvas = GameObject.FindGameObjectsWithTag("Health");
        
        DontDestroyOnLoad(this.gameObject);
    }
}
