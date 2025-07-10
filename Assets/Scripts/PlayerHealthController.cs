
using System.Collections;
using UnityEngine;


public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController playerHealthController;


    [HideInInspector] public int maximumHealth = 100;

    [HideInInspector] public int currentHealth;

    [HideInInspector] public float bodyArmour = 1f;

    private float bodyArmourStrength;


    [HideInInspector] public float maximumStamina = 100f;

    public float currentStamina;

    [HideInInspector] public float energyRequiredToRun = 10f;

    [HideInInspector] public float staminaRechargeRate = 2f;

    [HideInInspector] public float halfStaminaRechargeRate = 1f;




    private void Awake()
    {
        playerHealthController = this;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (bodyArmourStrength > 0)
        {
            bodyArmourStrength -= Time.deltaTime;
        }
    }


    public void DamagePlayer(int damage)
    {
        if (bodyArmourStrength <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                //gameObject.SetActive(false);
                currentHealth = 0;
            }

            bodyArmourStrength = bodyArmour;

            UIController.uiController.healthBarSlider.value = currentHealth;

            float healthPercentage = (float)currentHealth / maximumHealth * 100f;

            UIController.uiController.healthText.text = $"{healthPercentage}%";
        }
    }


} // end of class
