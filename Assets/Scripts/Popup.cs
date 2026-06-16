using UnityEngine;

public class Popup : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void Close()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
