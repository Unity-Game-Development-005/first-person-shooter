
using UnityEngine;


public class PlayerAmmoController : MonoBehaviour
{
    public static PlayerAmmoController playerAmmoController;


    public int maximumAmmo = 10;

    public int currentAmmo;




    private void Awake()
    {
        playerAmmoController = this;
    }


} // end of class
