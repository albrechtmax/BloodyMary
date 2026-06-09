using Unity.VisualScripting;
using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    public GameObject bloodDropScene;
    private float lastUpdate;
    private float period = 1;

    private Transform markerStart, markerEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastUpdate = Time.fixedTime;
        markerStart = transform.Find("SpawnMarkerStart");
        markerEnd = transform.Find("SpawnMarkerEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime - lastUpdate > period)
        {
            GameObject drop = Instantiate(bloodDropScene, GetRandomPosition(), transform.rotation);
            drop.GetComponent<BloodDrop>().bloodGroup = BloodGroup.GetRandom();
            drop.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 1.0f;
            lastUpdate = Time.fixedTime;
        }

    }

    Vector2 GetRandomPosition()
    {
        float rand = Random.Range(0.0f, 1.0f);
        return markerStart.position * (1 - rand) + markerEnd.position * rand;
    }
}
