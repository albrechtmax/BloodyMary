using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrop : MonoBehaviour
{
    public float fallSpeed = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * fallSpeed;
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BloodBath")
        {
            Destroy(gameObject);
            // we effectively hit the player, reduce some HP counter or whatever
        }
        else
        {
            Destroy(gameObject);
            // we were hit by projectile, increment some point counter or whetever
        }
    }
}
