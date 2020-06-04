using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private NavMeshAgent agent = null;
	public Transform destinationpoint = null;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destinationpoint.position);
    }
}
