using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrop : MonoBehaviour
{
    public float fallSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
         // Check if the other object has a PlayerController2D component
        // if (other.GetComponent<PlayerController2D>() != null) {
            
        //     // Destroy the collectible
        //     Destroy(gameObject);

        //     // Instantiate the particle effect
        //     Instantiate(onCollectEffect, transform.position, transform.rotation);
        // }

        
    }
}
