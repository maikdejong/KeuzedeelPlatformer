using UnityEngine;

namespace Batteries
{
    public class CollectBattery : MonoBehaviour
    {
        [SerializeField] private int batteryValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ScoreManager.Instance.ChangeScore(batteryValue);
                gameObject.SetActive(false);
            }
        }
    }
}