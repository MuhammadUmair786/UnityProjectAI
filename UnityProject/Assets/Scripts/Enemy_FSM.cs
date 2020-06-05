using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_FSM : MonoBehaviour

{
    private CheckMyVision checkMyVision;
    private NavMeshAgent agent;
    private Transform playerTransform;

    private Transform patrolDestination;

    private Health playerHealth;

    public float maxDamage = 5f;

    // Enums to keep states
    public enum ENEMY_STATES { patrol, chase, attack, nearToEdge }

    
    // We need a property to get the current state
    [SerializeField]
    private ENEMY_STATES currentState;

    public ENEMY_STATES CurrentState {
        get 
		{ 
			return currentState; 
		}
        set {
            currentState = value;
            StopAllCoroutines();
            switch (currentState) {
                case ENEMY_STATES.patrol:
                    StartCoroutine (EnemyPatrol ());
                    break;
                case ENEMY_STATES.chase:
                    StartCoroutine (EnemyChase ());
                    break;
                case ENEMY_STATES.attack:
                    StartCoroutine (EnemyAttack ());
                    break;
				case ENEMY_STATES.nearToEdge:
                    StartCoroutine (GoNearToEdge ());
                    break;
            }
        }
    }

    private void Awake () {
        checkMyVision = GetComponent<CheckMyVision> ();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
        playerHealth = GameObject.Find("Player").GetComponent<Health> ();
        playerTransform = playerHealth.GetComponent<Transform> ();
    }
    // Start is called before the first frame update
    void Start () {
       
        GameObject[] destinations = GameObject.FindGameObjectsWithTag ("Dest");
        //int pathIndex = Random.Range(0, destinations.Length);
        patrolDestination = destinations[Random.Range(0,destinations.Length)].GetComponent<Transform>();
        //CurrentState = ENEMY_STATES.patrol;
		CurrentState = ENEMY_STATES.nearToEdge;
        
    }

	public IEnumerator GoNearToEdge () {
        print("Go Near to Edge"); 
        while (currentState == ENEMY_STATES.nearToEdge) {

			 Vector3[] positionArray = new [] { new Vector3(3,0,404), new Vector3(791,0,404), new Vector3(397,0,6), new Vector3(403,0,797) };

            agent.SetDestination(positionArray[Random.Range(0,positionArray.Length)]);
			CurrentState = ENEMY_STATES.chase;
            yield return null;
        }       
    }

    public IEnumerator EnemyPatrol () {
        print("Start Patroling"); 
        while (currentState == ENEMY_STATES.patrol) {
            checkMyVision.sensitivity = CheckMyVision.Sensitivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(patrolDestination.position);
            while (agent.pathPending ) {              
                yield return null;
            }
            if (checkMyVision.targetInSight) {
                agent.isStopped = true;
                CurrentState = ENEMY_STATES.chase;
                yield break;
            }
            yield return null;
        }       
    }

    public IEnumerator EnemyChase () {
        print("Chasing Start");
        while (currentState == ENEMY_STATES.chase)
        {
            checkMyVision.sensitivity = CheckMyVision.Sensitivity.LOW;            
            agent.isStopped = false;
			agent.SetDestination(checkMyVision.lastKnownSighting);
            while (agent.pathPending)
            {
                yield return null;
            } 
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                if (!checkMyVision.targetInSight)
                {
                    CurrentState = ENEMY_STATES.patrol;
                   yield break;
                }
                else
                {
                    CurrentState = ENEMY_STATES.attack;
					yield break;
                }
            }
            yield return null;
        }       
    }

    public IEnumerator EnemyAttack () {

		 print("Attacking enemy");
        while (currentState == ENEMY_STATES.attack)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
            while (agent.pathPending)
            {
                yield return null;
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                print("Distance is increasing now");
                CurrentState = ENEMY_STATES.patrol;
                yield break;
            }
            else
            {
                // Do something
                playerHealth.HealthPoints -= maxDamage ;
				Debug.Log("Points reduced! ");


				CurrentState = ENEMY_STATES.patrol;

                if(playerHealth.HealthPoints==0)
				{
					//Application.Quit();
					UnityEditor.EditorApplication.isPlaying = false;
					
					yield break;
				}
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update () {

    }
}