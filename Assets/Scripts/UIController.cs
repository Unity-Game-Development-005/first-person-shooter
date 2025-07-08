
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController uiController;


    public Slider healthBarSlider;

    public Slider staminaBarSlider;



    private void Awake()
    {
        uiController = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


} // end of class
