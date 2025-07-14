
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Rigidbody bulletRigidbody;

    public float bulletSpeed;

    public float bulletLifeTime;

    public int bulletDamage = 1;




    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }


    private void MoveBullet()
    {
        // move the bullet formard
        bulletRigidbody.linearVelocity = transform.forward * bulletSpeed;

        // count down the life duration of the bullet
        bulletLifeTime -= Time.deltaTime;

        // if the bullet's life duration is less than or equal to zero
        if (bulletLifeTime <= 0)
        {
            // destroy the bullet
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // if the bullet collides with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // damage the player
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(bulletDamage);
        }

        // and destroy the bullet
        Destroy(gameObject);
    }


} // end of class
