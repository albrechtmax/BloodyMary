using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float seconds = 60.0f;
    public UnityEvent onTimeout;

    private float start;
    private bool fired = false;

    void Start()
    {
        start = Time.time;
    }

    void Update()
    {
        UpdateText();

        if (start + seconds <= Time.time)
        {
            if (onTimeout != null && !fired)
                onTimeout.Invoke();
            fired = true;
        }
    }

    void UpdateText()
    {
        var timeLeft = seconds - (Time.time - start);
        var secondsLeft = timeLeft % 60.0f;
        var minutesLeft = (timeLeft - secondsLeft) / 60.0f;
        GetComponent<Text>().text = $"{(int)minutesLeft:d02}:{(int)secondsLeft:d02}";
    }
}
