using UnityEngine;

public class ProjectileClip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerExit2D(Collider2D other)
    {
        // destroy projectiles leaveing the playable area
        if (other.gameObject.CompareTag("BloodProjectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
