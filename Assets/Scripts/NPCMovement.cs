
using UnityEngine;

using UnityEngine.AI;


public class NPCMovement : MonoBehaviour
{
    // reference to the nav mesh agent
    private NavMeshAgent navmeshAgent;

    // reference to the player transform
    private Transform playerTransform;

    // calculates the direction to move the agent toward the player
    private Vector3 currentDestination;

    // the distance the player has to get to the agent before the agent starts to follow
    [SerializeField] private float followDistance = 10f;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set the reference to the nav mesh agent
        navmeshAgent = GetComponent<NavMeshAgent>();

        // set the reference to the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // calls the mave mesh agent states
    void Update()
    {
        // if the player moves withing the follow distance of the agent
        /*if (Vector3.Distance(playerTransform.position, transform.position) < followDistance)
        {
            // then follow the player
            FollowPlayer();
        }

        // otherwise
        else
        {
            // search for the player
            SearchForPlayer();
        }*/


        // check if enemy agent has been killed
        EnemyAgentDead();
    }


    private void FollowPlayer()
    {
        // get the location of the player so the agent can follow
        navmeshAgent.destination = playerTransform.position;
    }


    private void SearchForPlayer()
    {
        // if the agent gets too close to a randomly selected search position
        if (Vector3.Distance(currentDestination, transform.position) < 5)
        {
            // get a new random search position
            currentDestination = transform.position + (new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
        }

        // and move the agent to the position
        navmeshAgent.destination = currentDestination;
    }


    private void EnemyAgentDead()
    {
        /*if ()
        {

        }*/
    }


} // end of class
