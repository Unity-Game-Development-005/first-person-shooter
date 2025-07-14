
using UnityEngine;

using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    // reference to the nav mesh agent
    private NavMeshAgent enemyAgent;

    public GameObject bullet;

    public Transform firePoint;

    public Animator enemyAnimator;


    // reference to the player transform
    private Transform playerTransform;


    // calculates the direction to move the agent toward the player
    private Vector3 currentDestination;

    // the distance the player has to get to the agent before the agent starts to follow
    [SerializeField] private float followDistance = 10f;







    private bool chasing;

    public float distanceToChase = 10f;

    public float distanceToLose = 15f;

    public float distanceToStop = 2f;

    private Vector3 startPoint;

    private Vector3 targetPoint;

    public float keepChasingTime = 5f;

    private float chaseCounter;

    public float fireRate;

    public float waitBetweenShots = 2f;

    public float timeToShoot = 1f;

    private float fireCount;

    private float shotWaitCounter;

    private float shootTimeCounter;

    private bool wasShot;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set the reference to the nav mesh agent
        enemyAgent = GetComponent<NavMeshAgent>();

        // set the reference to the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        startPoint = transform.position;
    }


    // calls the mave mesh agent states
    void Update()
    {
        MoveEnemy();
    }


    private void MoveEnemy()
    {
        targetPoint = playerTransform.position;

        targetPoint.y = transform.position.y;

        // if the enemy is not chasing the player
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;

                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    enemyAgent.destination = startPoint;
                }
            }

            // if the enemy is close to the player
            if (enemyAgent.remainingDistance < 0.25f)
            {
                // stop moving/animating the enemy
                enemyAnimator.SetBool("isMoving", false);
            }

            // otherwise
            else
            {
                // move/animate the enemy
                enemyAnimator.SetBool("isMoving", true);
            }
        }

        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                enemyAgent.destination = targetPoint;
            }

            else
            {
                enemyAgent.destination = transform.position;
            }

            // if the player is out of range
            if (Vector3.Distance(transform.position, playerTransform.position) > distanceToLose)
            {
                // stop the enemy chasing the player
                chasing = false;

                chaseCounter = keepChasingTime;
            }

            else
            {
                wasShot = false;
            }

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }

                enemyAnimator.SetBool("isMoving", true);
            }

            else
            {
                if (playerTransform.gameObject.activeInHierarchy)
                {
                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(playerTransform.position + new Vector3(0f, 1.2f, 0f));

                            // check the angle to the player
                            Vector3 targetDir = playerTransform.position - transform.position;

                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {

                                Instantiate(bullet, firePoint.position, firePoint.rotation);

                                enemyAnimator.SetTrigger("fireShot");
                            }

                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }

                        enemyAgent.destination = transform.position;
                    }

                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }

                enemyAnimator.SetBool("isMoving", false);
            }
        }
    }


    public void GetShot()
    {
        wasShot = true;

        chasing = true;
    }


} // end of class
