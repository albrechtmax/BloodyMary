using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// for drop down menu in editor
public enum BloodGroupProxy
{
    OMinus,
    OPlus,
    APlus,
    AMinus,
    BPlus,
    BMinus,
    ABPlus,
    ABMinus
}

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

    public GameObject healthBar;
    public int maxHealth = 100;
    public int startHealth = 50;
    private int _health;

    public int health
    {
        get => _health;
        set
        {
            var delta = value - _health;
            _health = value;
            if (health > maxHealth)
            {
                health = maxHealth; // cap health
                if (onHealthFull != null) onHealthFull.Invoke();
            }
            if (health < 0)
            {
                health = 0;
                if (onHealthEmpty != null) onHealthEmpty.Invoke();
            }
            UpdateHealthbar();
            healthBar.GetComponent<HealthBar>().ShowDelta(delta);
        }
    }

    public GameObject bloodProjectileScene;
    public Text scoreLabel;


    public BloodGroupProxy chooseBloodGroup = BloodGroupProxy.APlus;
    public BloodGroup bloodGroup = BloodGroup.Ap;

    public UnityEvent onHealthFull;
    public UnityEvent onHealthEmpty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (chooseBloodGroup)
        {
            case BloodGroupProxy.OMinus: bloodGroup = BloodGroup.O; break;
            case BloodGroupProxy.OPlus: bloodGroup = BloodGroup.Op; break;
            case BloodGroupProxy.AMinus: bloodGroup = BloodGroup.A; break;
            case BloodGroupProxy.APlus: bloodGroup = BloodGroup.Ap; break;
            case BloodGroupProxy.BMinus: bloodGroup = BloodGroup.B; break;
            case BloodGroupProxy.BPlus: bloodGroup = BloodGroup.Bp; break;
            case BloodGroupProxy.ABMinus: bloodGroup = BloodGroup.AB; break;
            case BloodGroupProxy.ABPlus: bloodGroup = BloodGroup.ABp; break;
        }
        health = startHealth;
    }

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

            var audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            health += Constants.HealthShot;
        }
    }

    private void UpdateHealthbar()
    {
        if (healthBar != null)
        {
            healthBar.GetComponent<HealthBar>().SetPercentage((float)_health / (float)maxHealth);
        }
    }
}
