
using System.Collections;
using UnityEngine;


public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController playerHealthController;

    public int maximumHealth;

    public int currentHealth;

    public float maximumStamina;

    public float currentStamina;

    public float runEnergyUse;

    public float staminaRechargeRate;

    private Coroutine rechargeStamina;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    // recharge the player's stamina
    private IEnumerator RechargeStamina()
    {
        // wait for one second before recharging
        yield return new WaitForSeconds(1f);

        // while the player's current stamina is less than the player's maximum stamina
        while (currentStamina < maximumStamina)
        {
            // increase the player's stamina by the recharge rate divided by ten
            // effectively recharging every one tenth of a second
            currentStamina += staminaRechargeRate / 10f;

            // if the player's current stamina is greater than the player's maximum stamina
            if (currentStamina > maximumStamina)
            {
                // make the player's current stamina equal to the player's maximum stamina
                currentStamina = maximumStamina;
            }

            // update the stamina bar display
            // staminabar.fillamount = currentStamina / maximumStamina;

            // and wait for one tenth of a second
            yield return new WaitForSeconds(0.1f);
        }
    }


} // end of class
