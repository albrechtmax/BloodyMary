using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject bloodDropScene;
    private float lastUpdate;
    private float period = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastUpdate = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime - lastUpdate > period)
        {
            Instantiate(bloodDropScene, transform.position, transform.rotation);
            lastUpdate = Time.fixedTime;
        }

    }
}
