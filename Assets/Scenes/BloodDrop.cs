using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BloodDrop : MonoBehaviour
{
    public BloodGroup bloodGroup;
    public AudioClip audioGood;
    public AudioClip audioBad;

    [Header("Sprites for different blood groups")]
    public Sprite spriteOp;
    public Sprite spriteOn;
    public Sprite spriteAp;
    public Sprite spriteAn;
    public Sprite spriteBp;
    public Sprite spriteBn;
    public Sprite spriteABp;
    public Sprite spriteABn;

    private bool isFadingOut = false;
    private float fadeOutStart, fadeOutEnd;

    void Start()
    {
        Sprite sprite;
        switch (bloodGroup.ToString())
        {
            case "O+": sprite = spriteOp; break;
            case "O-": sprite = spriteOn; break;
            case "A+": sprite = spriteAp; break;
            case "A-": sprite = spriteAn; break;
            case "B+": sprite = spriteBp; break;
            case "B-": sprite = spriteBn; break;
            case "AB+": sprite = spriteABp; break;
            case "AB-": sprite = spriteABn; break;
            default: throw new ArgumentOutOfRangeException();
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        if (isFadingOut)
        {
            var t = (Time.unscaledTime - fadeOutStart) / (fadeOutEnd - fadeOutStart);
            var renderer = GetComponent<SpriteRenderer>();
            var color = renderer.color;
            color.a = 1 - t;
            renderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Shooter shooter = FindAnyObjectByType<Shooter>();
        if (other.gameObject.name == "BloodBath")
        {
            // we effectively hit the player, reduce some HP counter or whatever

            if (shooter.bloodGroup.CanGetDontationFrom(bloodGroup))
            {
                // correct donation
                GetComponent<SpriteRenderer>().color = Color.green;
                shooter.score += 100;
                shooter.health += 10;
                GetComponent<AudioSource>().PlayOneShot(audioGood);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                shooter.health -= 10;
                GetComponent<AudioSource>().PlayOneShot(audioBad);
            }
            StartCoroutine(FadeOutDestroy(1)); // long enough to fall out of the screen
        }
        else if (other.gameObject.CompareTag("BloodProjectile"))
        {
            // we were hit by projectile, increment some point counter or whetever
            if (shooter.bloodGroup.CanGetDontationFrom(bloodGroup))
            {
                // destroyed good blood
                shooter.score += 10;
            }
            else
            {
                // destroyed bad blood
                shooter.score += 50;
            }

            Destroy(gameObject);
            Destroy(other.gameObject);
            // TODO play some animation
            // TODO play sound
        }
        else if (other.gameObject.CompareTag("BloodDrop"))
        {
            GameObject higher = other.transform.position.y > transform.position.y ? other.gameObject : gameObject;
            GameObject lower = higher == gameObject ? other.gameObject : gameObject;

            float shift = lower.GetComponent<Collider2D>().bounds.max.y - higher.GetComponent<Collider2D>().bounds.min.y;
            higher.transform.position += shift * Vector3.up;
        }
        else
        {
            // ¯\_(ツ)_/¯
        }
    }

    private IEnumerator FadeOutDestroy(float after)
    {
        Destroy(GetComponent<Collider2D>());
        isFadingOut = true;
        fadeOutStart = Time.unscaledTime;
        fadeOutEnd = Time.unscaledTime + after;
        yield return new WaitForSeconds(after);
        Destroy(gameObject);
    }
}
