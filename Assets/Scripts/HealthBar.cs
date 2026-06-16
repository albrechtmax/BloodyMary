using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public void SetPercentage(float percentage)
    {
        transform.Find("HealthFull").GetComponent<Image>().fillAmount = percentage;
    }

    public void ShowDelta(int delta)
    {
        GameObject effect = Instantiate(transform.Find("HealthEffect").gameObject, transform);
        effect.GetComponent<HealthEffect>().delta = delta;
        effect.SetActive(true);
    }
}
