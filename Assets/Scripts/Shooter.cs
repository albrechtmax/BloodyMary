using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public int score = 0;

    public GameObject bloodProjectileScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Camera camera = FindAnyObjectByType<Camera>();
        Vector3 worldPoint = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -camera.transform.position.z));
        transform.up = worldPoint - transform.position;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Transform target = transform.Find("Circle");

            GameObject proj = Instantiate(bloodProjectileScene);
            proj.transform.position = target.position;
            proj.transform.up = transform.up;
            proj.GetComponent<Rigidbody2D>().linearVelocity = transform.up * 10.0f;
        }
    }
}
