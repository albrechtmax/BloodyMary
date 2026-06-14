using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public void SetPercentage(float percentage)
    {
        transform.Find("HealthFull").GetComponent<Image>().fillAmount = percentage;
    }
}
