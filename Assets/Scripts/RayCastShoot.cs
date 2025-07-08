
using System.Collections;
using UnityEngine;


public class RayCastShoot : MonoBehaviour
{
    public int gunDamage = 1;

    public float fireRate = 0.25f;

    public float weaponRange = 50f;

    public float hitForce = 100f;

    public Transform gunEnd;

    private Camera fpsCamera;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private AudioSource gunAudio;

    private LineRenderer laserLine;

    private float nextFire;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

        gunAudio = GetComponent<AudioSource>();

        fpsCamera = GetComponentInParent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());



            Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }

            else
            {
                laserLine.SetPosition(1, fpsCamera.transform.forward * weaponRange);
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        gunAudio.Play();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }


} // end of class
