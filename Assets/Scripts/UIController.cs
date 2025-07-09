
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    // make script accessible to other scripts
    public static UIController uiController;


    // health bar
    public Slider healthBarSlider;

    public TMP_Text healthText;


    // stamina bar
    public Slider staminaBarSlider;


    // ammo bar
    public Slider ammoBarSlider;

    public TMP_Text ammoText;



    private void Awake()
    {
        uiController = this;
    }


} // end of class
