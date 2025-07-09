
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


    void Update()
    {
        // if player presses the fire button and is able to fire
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            // and the player still has ammo
            if (PlayerAmmoController.playerAmmoController.currentAmmo > 0)
            {
                nextFire = Time.time + fireRate;

                StartCoroutine(LaserEffect());

                // subtract one from ammo count
                PlayerAmmoController.playerAmmoController.currentAmmo--;

                UIController.uiController.ammoBarSlider.value = PlayerAmmoController.playerAmmoController.currentAmmo;

                UIController.uiController.ammoText.text = "AMMO: " + PlayerAmmoController.playerAmmoController.currentAmmo;


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

                    RagdollToggle ragdollSwitcher = hit.collider.GetComponent<RagdollToggle>();

                    if (ragdollSwitcher != null)
                    {
                        ragdollSwitcher.TriggerRagdoll();
                    }
                }

                else
                {
                    laserLine.SetPosition(1, fpsCamera.transform.forward * weaponRange);
                }
            }
        }
    }


    private IEnumerator LaserEffect()
    {
        gunAudio.Play();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }


} // end of class
