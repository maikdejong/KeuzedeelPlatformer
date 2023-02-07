using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public HealthSystem playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        //TotalHealth img is 10 hartjes. startingHealth is 3. 3/10hartjes is 3 hartjes.
        totalHealthBar.fillAmount = playerHealth.startingHealth / 10;
    }

    private void Update()
    {
        //CurrentHealth is dezelfde img maar dan met gekleurde hartjes.
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}