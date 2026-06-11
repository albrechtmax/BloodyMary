using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreLabel.text = "" + _score;
        }
    }

    public GameObject bloodProjectileScene;
    public Text scoreLabel;

    public BloodGroup bloodGroup = BloodGroup.A;

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
