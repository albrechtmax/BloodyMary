using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrop : MonoBehaviour
{
    public BloodGroup bloodGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BloodBath")
        {
            // we effectively hit the player, reduce some HP counter or whatever
            Destroy(gameObject);
            Shooter shooter = FindAnyObjectByType<Shooter>();
            shooter.score -= 1;
        }
        else if (other.gameObject.CompareTag("BloodProjectile"))
        {
            // we were hit by projectile, increment some point counter or whetever
            Destroy(gameObject);
            Destroy(other.gameObject);

            Shooter shooter = FindAnyObjectByType<Shooter>();
            shooter.score += 1;
        }
        else
        {
            // ¯\_(ツ)_/¯
        }
    }
}
