
using System.Collections;
using UnityEngine;


public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController playerHealthController;


    public int maximumHealth = 100;

    public int currentHealth;

    public float bodyArmour = 1f;

    private float bodyArmourStrength;

    public float maximumRunStamina = 100f;

    public float currentRunStamina;

    public float runEnergyUse = 25f;

    public float staminaRechargeRate = 10f;

    public Coroutine rechargeRunStamina;



    private void Awake()
    {
        playerHealthController = this;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentHealth = maximumHealth;

        //UIController.uiController.healthBarSlider.maxValue = maximumHealth;

        //UIController.uiController.healthBarSlider.value = currentHealth;

        //UIController.uiController.staminaBarSlider.maxValue = maximumRunStamina;

        //UIController.uiController.staminaBarSlider.value = currentRunStamina;
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
                gameObject.SetActive(false);
            }

            bodyArmourStrength = bodyArmour;
        }
    }


    // recharge the player's stamina
    public IEnumerator RechargeStamina()
    {
        // wait for one second before recharging
        yield return new WaitForSeconds(1f);

        // while the player's current stamina is less than the player's maximum stamina
        while (currentRunStamina < maximumRunStamina)
        {
            // increase the player's stamina by the recharge rate divided by ten
            // effectively recharging every one tenth of a second
            currentRunStamina += staminaRechargeRate / 10f;

            // if the player's current stamina is greater than the player's maximum stamina
            if (currentRunStamina > maximumRunStamina)
            {
                // make the player's current stamina equal to the player's maximum stamina
                currentRunStamina = maximumRunStamina;
            }

            // update the stamina bar display
            // staminabar.fillamount = currentStamina / maximumStamina;
            UIController.uiController.staminaBarSlider.value = currentRunStamina / maximumRunStamina;

            // and wait for one tenth of a second
            yield return new WaitForSeconds(0.1f);
        }
    }


} // end of class
