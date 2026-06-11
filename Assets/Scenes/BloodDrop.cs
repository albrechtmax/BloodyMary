using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodDrop : MonoBehaviour
{
    public BloodGroup bloodGroup;

    void Start()
    {
        transform.Find("Text").GetComponent<TextMesh>().text = bloodGroup.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BloodBath")
        {
            // we effectively hit the player, reduce some HP counter or whatever
            Shooter shooter = FindAnyObjectByType<Shooter>();

            if (shooter.bloodGroup.CanGetDontationFrom(bloodGroup))
            {
                // correct donation
                GetComponent<SpriteRenderer>().color = Color.green;
                shooter.score += 1;
                // TODO play eat/positive/... sound
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                shooter.score -= 1;
                // TODO play hit/negative/... sound
            }
            StartCoroutine(TimedDestroy(4)); // long enough to fall out of the screen
        }
        else if (other.gameObject.CompareTag("BloodProjectile"))
        {
            // we were hit by projectile, increment some point counter or whetever
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

    private IEnumerator TimedDestroy(float after)
    {
        Destroy(GetComponent<Collider2D>());
        yield return new WaitForSeconds(after);
        Destroy(gameObject);
    }
}
