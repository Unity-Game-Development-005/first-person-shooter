
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class EnemyHealthController : MonoBehaviour 
{
	// enemy health
	public int currentHealth = 5;

    // reference to enemy controller script
    public EnemyController enemyController;



	public void DamageEnemy(int damageAmount)
	{
        // subtract damage amount
        currentHealth -= damageAmount;

        if (enemyController != null)
        {
            enemyController.GetShot();
        }

        // if the enemie's health is less than or equal to zero
        if (currentHealth <= 0) 
		{
            // destroy the enemy
            //Destroy(gameObject);


            /*RagdollToggle ragdollSwitcher = GetComponent<RagdollToggle>();

            //if (ragdollSwitcher != null)
            //{
                ragdollSwitcher.TriggerRagdoll();
            //}*/

        }
    }


} // end of class
