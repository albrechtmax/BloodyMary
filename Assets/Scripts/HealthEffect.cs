using UnityEngine;
using UnityEngine.UI;

public class HealthEffect : MonoBehaviour
{
    public Color effectPositive = Color.darkGreen;
    public Color effectNegative = Color.darkRed;

    public int delta = 0;

    public AnimationCurve alphaCurve = AnimationCurve.Linear(0.0f, 1.0f, 2.0f, 0.0f);

    private float start;

    void Start()
    {
        // start time for animations
        start = Time.time;

        // text and base color from specified delta
        Text text = GetComponent<Text>();
        text.text = $"{delta}";
        if (delta >= 0)
        {
            text.text = "+" + text.text;
            text.color = effectPositive;
        }
        else
        {
            text.color = effectNegative;
        }

        // schedule to destroy when effect done
        if (alphaCurve.keys.Length == 0)
        {
            // no effect just destroy
            Destroy(gameObject);
        }
        else
        {
            var lastKey = alphaCurve.keys[alphaCurve.keys.Length - 1];
            Destroy(gameObject, Time.time + lastKey.time);
        }
    }

    void Update()
    {
        // text alpha from alpha curve
        Text text = GetComponent<Text>();
        var color = text.color;
        color.a = alphaCurve.Evaluate(Time.time - start);
        text.color = color;

        // float up
        RectTransform rect = GetComponent<RectTransform>();
        rect.Translate(10.0f * Time.deltaTime * Vector3.up);
    }
}
